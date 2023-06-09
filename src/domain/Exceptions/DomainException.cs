using System.Runtime.Serialization;

namespace Domain.Exceptions;

[Serializable]
public abstract class DomainException : Exception
{
    protected DomainException(string message) : base(message)
    {
    }

    protected DomainException(SerializationInfo info, StreamingContext content) : base(info, content)
    {
    }
}