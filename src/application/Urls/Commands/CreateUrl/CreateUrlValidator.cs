using FluentValidation;

namespace Application.Urls.Commands.CreateUrl
{
    internal class CreateUrlValidator : AbstractValidator<CreateUrlCommand>
    {
        public CreateUrlValidator()
        {
            RuleSet("Add", () =>
            {
                RuleFor(x => x.OriginalUrl)
                .NotEmpty()
                .WithMessage("{PropertyName} não pode ficar vazia.");
            });
        }
    }
}