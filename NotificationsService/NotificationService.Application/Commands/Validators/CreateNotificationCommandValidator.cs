using FluentValidation;

namespace NotificationService.Application.Commands.Validators
{
    public class CreateNotificationCommandValidator : AbstractValidator<CreateNotificationCommand>
    {
        public CreateNotificationCommandValidator()
        {
            RuleFor(r => r.Title)
                .MaximumLength(200).WithMessage("Длина не должна быть больше 200 символов");
            RuleFor(r => r.Message)
                .NotEmpty()
                .MaximumLength(2000).WithMessage("Лимит символов в сообщении 2000");
            RuleFor(r => r.UserId)
                .NotEmpty().WithMessage("Id пользователя обязателен")
                .Must(id => id != Guid.Empty).WithMessage("Неверный формат Id");
        }
    }
}
