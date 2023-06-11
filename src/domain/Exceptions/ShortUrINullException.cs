namespace Domain.Exceptions
{
    internal sealed class ShortUrINullException : DomainException
    {
        public ShortUrINullException() : base("O campo ShortUrl está vazio. Verifique.")
        {
        }
    }
}