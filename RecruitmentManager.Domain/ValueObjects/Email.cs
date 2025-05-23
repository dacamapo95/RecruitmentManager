using RecruitmentManager.Domain.Results;

namespace RecruitmentManager.Domain.ValueObjects;

public sealed record Email
{
    public string Value { get; }

    private Email() { }

    private Email(string value)
    {
        Value = value;
    }

    public static Result<Email> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return new Error("Email.Empty", "Email cannot be empty.");

        if (!IsValidEmail(value))
            return new Error("Email.InvalidFormat", "Invalid email format.");

        return new Email(value);
    }

    public override string ToString() => Value;

    private static bool IsValidEmail(string email)
    {
        return System.Text.RegularExpressions.Regex.IsMatch(
            email,
            @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
            System.Text.RegularExpressions.RegexOptions.IgnoreCase
        );
    }
}
