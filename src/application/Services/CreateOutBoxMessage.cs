using Domain.Entities;
using Newtonsoft.Json;

namespace Application.Services
{
    internal static class CreateOutBoxMessage
    {
        public static OutBoxMessage Handler(Url url)
        {
            return new OutBoxMessage
            {
                Id = Guid.NewGuid(),
                OccurredOn = DateTime.UtcNow,
                Content = JsonConvert.SerializeObject(url)
            };
        }
    }
}