using RecruitmentManager.Domain.Primitives;

namespace RecruitmentManager.Domain.Countries;

public class Country : MasterEntity<Guid>
{
    public ICollection<City> Cities { get; } = [];
}
