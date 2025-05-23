using RecruitmentManager.Domain.Results;

namespace RecruitmentManager.Domain.ValueObjects;

public record Period
{
    public DateTime BeginDate { get; }
    public DateTime? EndDate { get; }

    private Period() { }

    private Period(DateTime startDate, DateTime? endDate)
    {
        BeginDate = startDate;
        EndDate = endDate;
    }

    public static Result<Period> Create(DateTime startDate, DateTime? endDate)
    {
        if (endDate.HasValue && endDate.Value < startDate)
            return new Error(
                "Period.EndDate.BeforeBeginDate",
                "La fecha de fin no puede ser anterior a la fecha de inicio");

        return new Period(startDate, endDate);
    }

    public TimeSpan? Duration => EndDate.HasValue ? EndDate.Value - BeginDate : null;

    public int? DaysCount => Duration.HasValue ? (int)Duration.Value.TotalDays + 1 : null;

    public override string ToString()
    {
        return EndDate.HasValue 
            ? $"{BeginDate:yyyy-MM-dd} a {EndDate.Value:yyyy-MM-dd}" 
            : $"{BeginDate:yyyy-MM-dd} - Present";
    }
}