using Maven.Web.Middleware;
using Maven.Infraestructure.MavenData;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;
using System.Text;

// DI Usuario
using Maven.Application.Services.Interfaces;
using Maven.Application.Services.Implementations;
using Maven.Infraestructure.Repository.Interfaces;
using Maven.Infraestructure.Repository.Implementations;



Directory.CreateDirectory("Logs");

// =======================
// Configurar Serilog
// =======================
var logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
    .MinimumLevel.Override("Microsoft.EntityFrameworkCore.Database.Command", LogEventLevel.Information)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.Logger(l => l
        .Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Information)
        .WriteTo.File(@"Logs\Info-.log",
            shared: true,
            encoding: Encoding.UTF8,
            rollingInterval: RollingInterval.Day))
    .WriteTo.Logger(l => l
        .Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Warning)
        .WriteTo.File(@"Logs\Warning-.log",
            shared: true,
            encoding: Encoding.UTF8,
            rollingInterval: RollingInterval.Day))
    .WriteTo.Logger(l => l
        .Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Error)
        .WriteTo.File(@"Logs\Error-.log",
            shared: true,
            encoding: Encoding.UTF8,
            rollingInterval: RollingInterval.Day))
    .WriteTo.Logger(l => l
        .Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Fatal)
        .WriteTo.File(@"Logs\Fatal-.log",
            shared: true,
            encoding: Encoding.UTF8,
            rollingInterval: RollingInterval.Day))
    .CreateLogger();

Log.Logger = logger;

var builder = WebApplication.CreateBuilder(args);

// Integrar Serilog al host
builder.Host.UseSerilog(Log.Logger);

// MVC
builder.Services.AddControllersWithViews();

// =======================
// Dependency Injection (Maven)
// =======================

// Repositories
builder.Services.AddScoped<IRepositoryUsuario, RepositoryUsuario>();

// Services
builder.Services.AddScoped<IServiceUsuario, ServiceUsuario>();

// Repositories
builder.Services.AddScoped<IRepositoryJoya, RepositoryJoya>();

// Services
builder.Services.AddScoped<IServiceJoya, ServiceJoya>();



// =======================
// AutoMapper (si luego lo ocupas)
// =======================
// builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// =======================
// Configurar SQL Server Maven DbContext
// =======================
var connectionString = builder.Configuration.GetConnectionString("SqlServerDataBase");
if (string.IsNullOrWhiteSpace(connectionString))
{
    throw new InvalidOperationException(
        "No se encontró la cadena de conexión 'SqlServerDataBase' en appsettings.json / appsettings.Development.json.");
}

builder.Services.AddDbContext<MavenContext>(options =>
{
    options.UseSqlServer(connectionString, sqlOptions =>
    {
        sqlOptions.EnableRetryOnFailure();
    });

    if (builder.Environment.IsDevelopment())
        options.EnableSensitiveDataLogging();
});

var app = builder.Build();

// =======================
// Pipeline
// =======================
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
else
{
    app.UseMiddleware<ErrorHandlingMiddleware>();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseSerilogRequestLogging();

app.UseAuthorization();

app.MapStaticAssets();
app.UseAntiforgery();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
