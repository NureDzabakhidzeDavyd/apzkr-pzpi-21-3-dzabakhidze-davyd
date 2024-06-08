using FluentValidation.Results;

namespace Kolosok.Application.Validators;

/// <summary>
/// Represents an exception for validation failures.
/// </summary>
public class ValidationException : ApplicationException
{
    /// <summary>
    /// Gets or sets the dictionary of validation errors.
    /// The key represents the property name, and the value represents the array of error messages for that property.
    /// </summary>
    public Dictionary<string, string[]> Errors { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ValidationException"/> class.
    /// </summary>
    public ValidationException() : base("One or more validation failures have occurred.")
    {
        Errors = new Dictionary<string, string[]>();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ValidationException"/> class with the specified validation failures.
    /// </summary>
    /// <param name="failures">The validation failures.</param>
    public ValidationException(IEnumerable<ValidationFailure> failures)
        : this()
    {
        Errors = failures
            .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
            .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
    }
}