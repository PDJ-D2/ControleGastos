namespace Domain.Exceptions;

// Exceção usada para violações de regras de negócio

public class DomainException : Exception
{
    public DomainException(string message) : base(message)
    {
    }
}
