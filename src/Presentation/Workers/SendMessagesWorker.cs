using Domain.Messaging;
using Domain.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Presentation.Workers
{
    public sealed class SendMessagesWorker : BackgroundService
    {
        private readonly ILogger<SendMessagesWorker> _logger;
        private readonly IServiceScopeFactory _scopeFactory;

        public SendMessagesWorker(ILogger<SendMessagesWorker> logger,
               IServiceScopeFactory scopeFactory)
        {
            _logger = logger;
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while(!stoppingToken.IsCancellationRequested)
            {
                using(var scope = _scopeFactory.CreateScope())
                {
                    var repository = scope.ServiceProvider.GetRequiredService<IOutboxMessageRepository>();
                    var messageBusService = scope.ServiceProvider.GetRequiredService<IMessageBusService>();

                    _logger.LogInformation("Pesquisa no banco de dados novas mensagens");
                    var messages = await repository.GetOutBoxMessagesAsync(stoppingToken);

                    _logger.LogInformation("Envia as mensagens para o broker e atualiza o banco de dados");
                    foreach(var message in messages)
                    {
                        messageBusService.Publish(message.Content, "Add-Url");
                        await repository.UpdateOutBoxMessageAsync(message.Id, stoppingToken);
                    }
                }

                await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
            }
        }
    }
}