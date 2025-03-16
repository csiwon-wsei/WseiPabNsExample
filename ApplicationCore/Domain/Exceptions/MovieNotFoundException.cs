namespace ApplicationCore.Domain.Exceptions;

public class MovieNotFoundException(string message): Exception(message)
{
    
}