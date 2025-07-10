using System.Net;

namespace NotificationService.API.DTO_s
{
    /// <summary>
    /// Data Transfer Object (DTO) с описанием ошибки.
    /// </summary>
    public class ErrorDTO
    {
        /// <summary>
        /// Статус код
        /// </summary>
        public HttpStatusCode StatusCode { get; set; }

        /// <summary>
        /// Сообщение об ошибке
        /// </summary>
        public string ExMessage {  get; set; } = string.Empty;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="statusCode">Статус код</param>
        /// <param name="exMessage">Сообщение об ошибке</param>
        public ErrorDTO(HttpStatusCode statusCode, string exMessage)
        {
            StatusCode = statusCode;
            ExMessage = exMessage;
        }
    }
}
