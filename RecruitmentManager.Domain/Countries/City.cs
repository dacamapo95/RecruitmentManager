using RecruitmentManager.Domain.Primitives;

namespace RecruitmentManager.Domain.Countries;

public class City : MasterEntity<Guid>
{
    public Guid CountryId { get; set; }

    public Country Country { get; set; }
}