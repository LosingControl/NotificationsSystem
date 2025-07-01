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
        public Guid Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string? DeviceToken { get; set; }

        public List<Notification> Notifications { get; set; } = new();
    }
}
