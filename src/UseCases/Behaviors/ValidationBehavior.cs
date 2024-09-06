#region

using Domain.Exceptions;
using FluentValidation;
using MediatR;
using ValidationException = Domain.Exceptions.ValidationException;
using ValidationResult = FluentValidation.Results.ValidationResult;

#endregion

namespace UseCases.Behaviors;

public sealed class ValidationBehavior<TRequest, TResponse>(
    IEnumerable<IValidator<TRequest>> validators) : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (!validators.Any()) return await next();

        var context = new ValidationContext<TRequest>(request);
        var validationFailures = await Task.WhenAll(
            validators.Select(validator => validator.ValidateAsync(context, cancellationToken)));
        ThrowExceptionIfInValid(validationFailures);
        var response = await next();
        return response;
    }

    private static void ThrowExceptionIfInValid(IEnumerable<ValidationResult> validationFailures)
    {
        var errors = validationFailures
            .Where(validationResult => !validationResult.IsValid)
            .SelectMany(validationResult => validationResult.Errors)
            .Select(validationFailure => new ValidationError(
                validationFailure.PropertyName,
                validationFailure.ErrorMessage))
            .ToList();

        if (errors.Count != 0) throw new ValidationException(errors);
    }
}
