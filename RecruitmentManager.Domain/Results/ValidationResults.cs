namespace RecruitmentManager.Domain.Results;

public interface IValidationResults
{
    public static readonly Error Error =
        new("Validation.Error", "A validation problem ocurred");

    Error[] Errors { get; }
}

public class ValidationResult : Result, IValidationResults
{
    private ValidationResult(Error[] errors)
        : base(false, IValidationResults.Error) => Errors = errors;

    public Error[] Errors { get; }

    public static ValidationResult WithErrors(Error[] errors) => new(errors);
}

public  class ValidationResult<T> : Result<T>, IValidationResults
{
    private ValidationResult(Error[] errors) : base(false, default, IValidationResults.Error)
    {
        Errors = errors;
    }

    public static ValidationResult<T> WithErrors(Error[] errors) => new(errors);

    public Error[] Errors { get; }
}
