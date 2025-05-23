using RecruitmentManager.Domain.Results;

namespace RecruitmentManager.Domain.ValueObjects;

public record FullName
{
    public string FirstName { get; }
    public string SurName { get; }

    private FullName() { }

    private FullName(string firstName, string lastName)
    {
        FirstName = firstName.Trim();
        SurName = lastName.Trim();
    }

    public static Result<FullName> Create(string firstName, string lastName)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            return new Error("FullName.FirstName.Empty", "El nombre no puede estar vacío");

        if (string.IsNullOrWhiteSpace(lastName))
            return new Error("FullName.SurName.Empty", "El apellido no puede estar vacío");

        return new FullName(firstName, lastName);
    }

    public string GetFullName() => $"{FirstName} {SurName}";

    public string GetSortableName() => $"{SurName}, {FirstName}";

    public static implicit operator string(FullName name) => name.GetFullName();

}