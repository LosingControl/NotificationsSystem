using FluentValidation;
using NotificationService.API.DTO_s;
using System.Net;

namespace NotificationService.API.Middlewares
{
    /// <summary>
    /// Middleware для обработки исключений.
    /// Логирует ошибки и возвращает JSON-ответ клиенту.
    /// </summary>
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(
            RequestDelegate next, 
            ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        /// <summary>
        /// Обрабатывает HTTP-запрос и перехватывает исключения.
        /// </summary>
        /// <param name="httpContext">Контекст HTTP-запроса.</param>
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (ValidationException ex)
            {
                await HandleExceptionAsync(
                    httpContext,
                    ex.Message,
                    HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {

                await HandleExceptionAsync(
                    httpContext,
                    ex.Message,
                    HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Обрабатывает исключение, логирует его и отправляет клиенту JSON-ответ с описанием ошибки.
        /// </summary>
        /// <param name="httpContext">Контекст HTTP-запроса.</param>
        /// <param name="exMessage">Сообщение исключения.</param>
        /// <param name="httpStatus">HTTP-статус код для ответа.</param>
        private async Task HandleExceptionAsync(
            HttpContext httpContext,
            string exMessage,
            HttpStatusCode httpStatus)
        {
            _logger.LogError(exMessage);

            HttpResponse httpResponse = httpContext.Response;
            httpResponse.ContentType = "application/json";
            httpResponse.StatusCode = (int)httpStatus;

            ErrorDTO errorDTO = new ErrorDTO(httpStatus, exMessage);

            await httpResponse.WriteAsJsonAsync(errorDTO);
        }

    }
}
