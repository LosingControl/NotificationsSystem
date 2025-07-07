namespace NotificationService.Domain.Entities
{
    /// <summary>
    /// Модель пользователя системы
    /// </summary>
    /// <remarks>
    /// Содержит основные данные пользователя и связанные с ним уведомления.
    /// <para><b>Особенности работы:</b></para>
    /// <list type="number">
    ///   <item>Используется для системы уведомлений и аутентификации</item>
    ///   <item>Поддерживает хранение токена устройства для push-уведомлений</item>
    ///   <item>Содержит историю всех уведомлений пользователя</item>
    /// </list>
    /// </remarks>
    public class User
    {
        /// <summary>
        /// Уникальный идентификатор пользователя
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Email адрес пользователя
        /// </summary>
        /// <remarks>
        /// Используется для email-уведомлений
        /// </remarks>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Токен устройства для push-уведомлений
        /// </summary>
        /// <remarks>
        /// Может быть null
        /// </remarks>
        public string? DeviceToken { get; set; }

        /// <summary>
        /// Коллекция уведомлений пользователя
        /// </summary>
        /// <remarks>
        /// Навигационное свойство для EF Core
        /// </remarks>
        public List<Notification> Notifications { get; set; } = new();

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <param name="email">Email адрес пользователя</param>
        /// <param name="deviceToken">Токен устройства</param>
        public User(Guid id, string email, string? deviceToken)
        {
            Id = id;
            Email = email;
            DeviceToken = deviceToken;
        }
    }
}
