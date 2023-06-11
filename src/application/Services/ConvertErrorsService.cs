using FluentValidation.Results;

namespace Application.Services
{
    public static class ConvertErrorsService
    {
        public static IEnumerable<string> GetError(List<ValidationFailure> errors)
        {
            List<string> errorList = new();

            foreach(var item in errors)
            {
                errorList.Add(item.ErrorMessage);
            }

            return errorList;
        }
    }
}