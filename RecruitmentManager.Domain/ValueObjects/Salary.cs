using RecruitmentManager.Domain.Results;

namespace RecruitmentManager.Domain.ValueObjects;

public record Salary
{
    public decimal Amount { get; }
    public string Currency { get; }

    private Salary() { }

    private Salary(decimal amount, string currency)
    {
        Amount = amount;
        Currency = currency.Trim().ToUpper();
    }

    public static Result<Salary> Create(decimal amount, string currency)
    {
        if (amount < 0)
            return new Error(
                "Salary.Amount.Negative", "El salario no puede ser negativo");

        if (string.IsNullOrWhiteSpace(currency) || currency.Trim().Length != 3)
            return new Error(
                    "Salary.Currency.Invalid",
                    "La moneda debe ser un código ISO de 3 letras");

        if (!currency.Trim().All(char.IsLetter))
            return new Error(
                "Salary.Currency.InvalidFormat",
                "La moneda debe contener solo letras");


        return new Salary(amount, currency);
    }
}