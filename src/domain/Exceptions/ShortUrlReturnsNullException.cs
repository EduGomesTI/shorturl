namespace Domain.Exceptions
{
    public class ShortUrlReturnsNullException : DomainException
    {
        public ShortUrlReturnsNullException(string message) : base(message)
        {
        }
    }
}