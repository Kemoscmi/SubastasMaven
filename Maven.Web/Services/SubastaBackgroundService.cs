using Maven.Application.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Maven.Web.Services
{
    public class SubastaBackgroundService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<SubastaBackgroundService> _logger;

        public SubastaBackgroundService(
            IServiceScopeFactory scopeFactory,
            ILogger<SubastaBackgroundService> logger)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("SubastaBackgroundService iniciado.");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using var scope = _scopeFactory.CreateScope();
                    var serviceSubasta = scope.ServiceProvider.GetRequiredService<IServiceSubasta>();

                    var activadas = await serviceSubasta.ActivarPublicadasAsync();
                    var cerradas = await serviceSubasta.CerrarSubastasVencidasAsync();

                    if (activadas > 0 || cerradas > 0)
                    {
                        _logger.LogInformation(
                            "Subastas procesadas. Activadas: {Activadas}. Cerradas: {Cerradas}.",
                            activadas,
                            cerradas);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error en SubastaBackgroundService.");
                }

                await Task.Delay(TimeSpan.FromSeconds(8), stoppingToken);
            }

            _logger.LogInformation("SubastaBackgroundService finalizado.");
        }
    }
}