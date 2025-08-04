using FluentValidation;
using MediatR;

namespace NotificationService.Application.Validations
{
    /// <summary>
    /// Осуществляет валидацию входящих запросов перед их обработкой.
    /// </summary>
    /// <typeparam name="TRequest">Тип запроса."/>).</typeparam>
    /// <typeparam name="TResponse">Тип ответа.</typeparam>
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        /// <summary>
        /// Проверяет валидность запроса перед передачей его в обработчик.
        /// Если запрос невалиден, генерирует исключение <see cref="ValidationException"/>.
        /// </summary>
        /// <param name="request">Входящий запрос.</param>
        /// <param name="next">Делегат для вызова следующего обработчика в конвейере.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Ответ, сгенерированный обработчиком запроса, если валидация прошла успешно.</returns>
        /// <exception cref="ValidationException">Выбрасывается, если запрос не прошел валидацию.</exception>
        public async Task<TResponse> Handle(
            TRequest request, 
            RequestHandlerDelegate<TResponse> next, 
            CancellationToken cancellationToken)
        {
            var failures = _validators
                .Select(x => x.Validate(request))
                .SelectMany(x => x.Errors)
                .Where(x => x != null)
                .ToList();

            if (failures.Count != 0)
            {
                throw new ValidationException(failures);
            }

            return await next(cancellationToken);
        }
    }
}
