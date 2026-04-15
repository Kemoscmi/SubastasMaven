using Maven.Application.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.SignalR;
using Maven.Web.Hubs;

namespace Maven.Web.Services
{
    public class SubastaBackgroundService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<SubastaBackgroundService> _logger;
        private readonly IHubContext<SubastaHub> _hubContext;

        public SubastaBackgroundService(
            IServiceScopeFactory scopeFactory,
            ILogger<SubastaBackgroundService> logger,
            IHubContext<SubastaHub> hubContext)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
            _hubContext = hubContext;
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

                    foreach (var subasta in activadas)
                    {
                        await _hubContext.Clients.Group($"subasta-{subasta.SubastaId}")
                            .SendAsync("SubastaActivada", new
                            {
                                estado = subasta.Estado
                            }, stoppingToken);
                    }

                    foreach (var subasta in cerradas)
                    {
                        await _hubContext.Clients.Group($"subasta-{subasta.SubastaId}")
                            .SendAsync("SubastaFinalizada", new
                            {
                                estado = subasta.Estado,
                                usuarioGanador = subasta.UsuarioGanador,
                                montoFinal = subasta.MontoFinal,
                                sinPujas = subasta.SinPujas
                            }, stoppingToken);
                    }

                    if (activadas.Count > 0 || cerradas.Count > 0)
                    {
                        _logger.LogInformation(
                            "Subastas procesadas. Activadas: {Activadas}. Cerradas: {Cerradas}.",
                            activadas.Count,
                            cerradas.Count);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error en SubastaBackgroundService.");
                }

                await Task.Delay(TimeSpan.FromSeconds(1), stoppingToken);
            }

            _logger.LogInformation("SubastaBackgroundService finalizado.");
        }
    }
}