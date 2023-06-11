using FluentValidation;

namespace Application.Urls.Commands.CallOriginalUrl
{
    internal class CallOriginalUrlValidator : AbstractValidator<CallOriginalUrlCommand>
    {
        public CallOriginalUrlValidator()
        {
            RuleSet("Add", () =>
            {
                RuleFor(x => x.ShortUrl)
                .NotEmpty()
                .WithMessage("{PropertyName} não pode ficar vazia");
            });
        }
    }
}