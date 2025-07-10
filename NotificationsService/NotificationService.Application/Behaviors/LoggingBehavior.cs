using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationService.Application.Behaviors
{
    /// <summary>
    /// Осуществляет логирование входящих запросов и исходящих ответов.
    /// </summary>
    /// <typeparam name="TRequest">Тип запроса."/>).</typeparam>
    /// <typeparam name="TResponse">Тип ответа.</typeparam>
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

        public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Обрабатывает запрос, логирует его данные, выполняет следующий делегат в конвейере и логирует ответ.
        /// </summary>
        /// <param name="request">Входящий запрос.</param>
        /// <param name="next">Делегат для вызова следующего обработчика в конвейере.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// /// <returns>Ответ, сгенерированный обработчиком запроса.</returns>
        public async Task<TResponse> Handle(
            TRequest request, 
            RequestHandlerDelegate<TResponse> next, 
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Запрос {typeof(TRequest).Name}: Данные {request}");
            var response = await next(cancellationToken);
            _logger.LogInformation($"Ответ {typeof(TResponse).Name}: Данные {response}");

            return response;
        }
    }
}
