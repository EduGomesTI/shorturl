using FluentValidation;

namespace Application.Urls.Commands.ValidateUrl
{
    internal class ValidateUrlValidator : AbstractValidator<ValidateUrlCommand>
    {
        public ValidateUrlValidator()
        {
            RuleSet("Add", () =>
            {
                RuleFor(x => x.ShortUrl)
                .NotEmpty()
                .WithMessage("{PropertyName} não pode ficar vazia.");
            });
        }
    }
}