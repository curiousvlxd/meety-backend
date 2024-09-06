namespace Domain.Exceptions;

public class ValidationException(IEnumerable<ValidationError> errors) : Exception("Validation failed")
{
    public IEnumerable<ValidationError> Errors { get; } = errors;
}
public record ValidationError(string PropertyName, string ErrorMessage);
