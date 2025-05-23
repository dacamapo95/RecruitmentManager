using RecruitmentManager.Domain.Countries;
using RecruitmentManager.Domain.Primitives;

namespace RecruitmentManager.Domain.Candidates;

public class Address : AuditableEntity<Guid>
{
    public string? Street { get; set; } 

    public Guid CityId { get; set; } = default!;

    public string? ZipCode { get; set; }

    public City City { get; set; }

    public Address()
    {
    }

    private Address(string? street, Guid cityId, string? zipCode)
    {
        Street = street;
        CityId = cityId;
        ZipCode = zipCode;
    }

    public static Address Create(string street, Guid cityId, string? zipCode) =>
        new(street, cityId, zipCode);
}