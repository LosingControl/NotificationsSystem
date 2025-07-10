using FluentValidation;

namespace NotificationService.Application.Queries.Validators
{
    public class GetNotificationsByFilterQueryValidator : AbstractValidator<GetNotificationsByFilterQuery>
    {
        public GetNotificationsByFilterQueryValidator()
        {
            RuleFor(r => r.Filters.PageNumber)
                .GreaterThan(0).WithMessage("Номер страницы должен быть положительным");

            RuleFor(r => r.Filters.PageSize)
                .InclusiveBetween(1, 100).WithMessage("Размер страницы должен быть от 1 до 100");

            RuleFor(r => r.Filters.Id)
                .Must(id => id != Guid.Empty).WithMessage("Неверный формат Id");
        }
    }
}
