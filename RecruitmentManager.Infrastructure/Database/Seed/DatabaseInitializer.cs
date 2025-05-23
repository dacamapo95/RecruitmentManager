using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RecruitmentManager.Domain.Candidates;
using RecruitmentManager.Domain.Countries;
using RecruitmentManager.Domain.Results;
using RecruitmentManager.Domain.ValueObjects;

namespace RecruitmentManager.Infrastructure.Database.Seed;

public class DatabaseInitializer(
    ApplicationDbContext context,
    ILogger<DatabaseInitializer> logger)
{
    private readonly ApplicationDbContext _context = context;
    private readonly ILogger<DatabaseInitializer> _logger = logger;
    private readonly Random _random = new();

    public async Task InitializeAsync()
    {
        try
        {
            _logger.LogInformation("Ensuring the database is created and applying migrations...");
            await _context.Database.MigrateAsync();
            _logger.LogInformation("Database migration completed successfully.");

            await InitializeCitiesAsync();
            await InitializeStatesAsync();
            await InitializeCandidatesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while migrating or ensuring the database is created.");
            throw;
        }
    }

    public async Task InitializeCitiesAsync()
    {
        if (!_context.Cities.Any())
        {

            var defaultCountry = new Country { Name = "Colombia" };

            _context.Countries.Add(defaultCountry);

            var cities = new List<City>
                {
                    new City { Name = "Bogotá", Country = defaultCountry },
                    new City { Name = "Medellín", Country = defaultCountry },
                    new City { Name = "Cali", Country = defaultCountry },
                    new City { Name = "Barranquilla", Country = defaultCountry },
                    new City { Name = "Cartagena", Country = defaultCountry },
                    new City { Name = "Bucaramanga", Country = defaultCountry },
                    new City { Name = "Pereira", Country = defaultCountry },
                    new City { Name = "Manizales", Country = defaultCountry },
                    new City { Name = "Santa Marta", Country = defaultCountry },
                    new City { Name = "Cúcuta", Country = defaultCountry },
                    new City { Name = "Ibagué", Country = defaultCountry },
                    new City { Name = "Neiva", Country = defaultCountry },
                    new City { Name = "Villavicencio", Country = defaultCountry },
                    new City { Name = "Pasto", Country = defaultCountry },
                    new City { Name = "Armenia", Country = defaultCountry },
                    new City { Name = "Montería", Country = defaultCountry },
                    new City { Name = "Sincelejo", Country = defaultCountry },
                    new City { Name = "Popayán", Country = defaultCountry },
                    new City { Name = "Tunja", Country = defaultCountry },
                    new City { Name = "Riohacha", Country = defaultCountry },
                    new City { Name = "Valledupar", Country = defaultCountry },
                    new City { Name = "Quibdó", Country = defaultCountry },
                    new City { Name = "Florencia", Country = defaultCountry },
                    new City { Name = "San Andrés", Country = defaultCountry },
                    new City { Name = "Leticia", Country = defaultCountry },
                    new City { Name = "Mocoa", Country = defaultCountry },
                    new City { Name = "Yopal", Country = defaultCountry },
                    new City { Name = "Arauca", Country = defaultCountry },
                    new City { Name = "Mitú", Country = defaultCountry },
                    new City { Name = "Puerto Carreño", Country = defaultCountry },
                    new City { Name = "Buenaventura", Country = defaultCountry },
                    new City { Name = "Tumaco", Country = defaultCountry },
                    new City { Name = "Turbo", Country = defaultCountry },
                    new City { Name = "Apartadó", Country = defaultCountry },
                    new City { Name = "Girardot", Country = defaultCountry },
                    new City { Name = "Zipaquirá", Country = defaultCountry },
                    new City { Name = "Soacha", Country = defaultCountry },
                    new City { Name = "Facatativá", Country = defaultCountry },
                    new City { Name = "Chía", Country = defaultCountry },
                    new City { Name = "Fusagasugá", Country = defaultCountry },
                    new City { Name = "Sogamoso", Country = defaultCountry },
                    new City { Name = "Duitama", Country = defaultCountry },
                    new City { Name = "Chiquinquirá", Country = defaultCountry },
                    new City { Name = "Ipiales", Country = defaultCountry },
                    new City { Name = "Tuluá", Country = defaultCountry },
                    new City { Name = "Palmira", Country = defaultCountry },
                    new City { Name = "Buga", Country = defaultCountry },
                    new City { Name = "Jamundí", Country = defaultCountry },
                    new City { Name = "Yumbo", Country = defaultCountry },
                    new City { Name = "Cartago", Country = defaultCountry },
                    new City { Name = "Rionegro", Country = defaultCountry },
                    new City { Name = "Envigado", Country = defaultCountry },
                    new City { Name = "Itagüí", Country = defaultCountry },
                    new City { Name = "Sabaneta", Country = defaultCountry },
                    new City { Name = "Bello", Country = defaultCountry },
                    new City { Name = "Copacabana", Country = defaultCountry },
                    new City { Name = "La Estrella", Country = defaultCountry },
                    new City { Name = "Caldas", Country = defaultCountry },
                    new City { Name = "Barbosa", Country = defaultCountry },
                    new City { Name = "Girardota", Country = defaultCountry },
                    new City { Name = "Caucasia", Country = defaultCountry },
                    new City { Name = "Apartadó", Country = defaultCountry },
                    new City { Name = "Turbo", Country = defaultCountry },
                    new City { Name = "Carepa", Country = defaultCountry },
                    new City { Name = "Chigorodó", Country = defaultCountry },
                    new City { Name = "Necoclí", Country = defaultCountry },
                    new City { Name = "Arboletes", Country = defaultCountry },
                    new City { Name = "San Pedro de Urabá", Country = defaultCountry },
                    new City { Name = "San Juan de Urabá", Country = defaultCountry },
                    new City { Name = "Mutatá", Country = defaultCountry },
                    new City { Name = "Murindó", Country = defaultCountry },
                    new City { Name = "Vigía del Fuerte", Country = defaultCountry },
                    new City { Name = "Carmen de Atrato", Country = defaultCountry },
                    new City { Name = "Bojayá", Country = defaultCountry },
                    new City { Name = "Medio Atrato", Country = defaultCountry },
                    new City { Name = "Medio Baudó", Country = defaultCountry },
                    new City { Name = "Medio San Juan", Country = defaultCountry },
                    new City { Name = "Nóvita", Country = defaultCountry },
                    new City { Name = "Río Iró", Country = defaultCountry },
                    new City { Name = "Río Quito", Country = defaultCountry },
                    new City { Name = "Riosucio", Country = defaultCountry },
                    new City { Name = "San José del Palmar", Country = defaultCountry },
                    new City { Name = "Sipí", Country = defaultCountry },
                    new() { Name = "Tadó", Country = defaultCountry },
                    new City { Name = "Unguía", Country = defaultCountry },
                    new City { Name = "Unión Panamericana", Country = defaultCountry }
                };

            _context.Cities.AddRange(cities);
            await _context.SaveChangesAsync();
        }
    }

    public async Task InitializeStatesAsync()
    {
        if (!_context.Set<State>().Any())
        {
            var stateTranslations = new Dictionary<StateEnum, string>
            {
                { StateEnum.New, "Nuevo" },
                { StateEnum.InReview, "En Revisión" },
                { StateEnum.InterviewScheduled, "Entrevista Programada" },
                { StateEnum.Interviewed, "Entrevistado" },
                { StateEnum.OfferSent, "Oferta Enviada" },
                { StateEnum.Hired, "Contratado" },
                { StateEnum.Rejected, "Rechazado" }
            };

            var states = Enum.GetValues<StateEnum>()
                .Select(stateEnum => new State
                {
                    Id = (int)stateEnum,
                    Name = stateTranslations[stateEnum]
                })
                .ToList();

            _context.Set<State>().AddRange(states);
            await _context.SaveChangesAsync();

            _logger.LogInformation("States have been initialized successfully with Spanish names.");
        }
    }

    public async Task InitializeCandidatesAsync()
    {
        if (!_context.Set<Candidate>().Any())
        {
            _logger.LogInformation("Initializing sample candidates...");

            var cities = await _context.Cities.Take(10).ToListAsync();
            var states = await _context.Set<State>().ToListAsync();

            if (!cities.Any() || !states.Any())
            {
                _logger.LogWarning("Unable to seed candidates: no cities or states found");
                return;
            }

            var candidates = new List<Candidate>();
            int successCount = 0;

            for (int i = 0; i < 20; i++)
            {
                var city = cities[_random.Next(cities.Count)];
                var state = states[_random.Next(states.Count)];

                var fullNameResult = GenerateRandomFullName();
                var emailResult = GenerateRandomEmail();
                var experiences = GenerateRandomExperiences(3);

                var candidate = new Candidate
                {
                    Id = Guid.NewGuid(),
                    FullName = fullNameResult.Value,
                    Email = emailResult.Value,
                    DateOfBirth = GenerateRandomDateOfBirth(),
                    PhoneNumber = GenerateRandomPhoneNumber(),
                    State = state,
                    StateId = state.Id,
                    Address = new Address
                    {
                        City = city,
                        Street = $"Calle {_random.Next(1, 100)} #{_random.Next(1, 100)}-{_random.Next(1, 100)}",
                        ZipCode = $"{_random.Next(10000, 99999)}"
                    },
                    Experiences = experiences
                };

                candidates.Add(candidate);
                successCount++;
            }

            if (candidates.Any())
            {
                _context.Set<Candidate>().AddRange(candidates);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Sample candidates initialized successfully: {count} candidates created", successCount);
            }
            else
            {
                _logger.LogWarning("No valid candidate data could be generated");
            }
        }
    }

    private Result<FullName> GenerateRandomFullName()
    {
        string[] firstNames = { "Juan", "Carlos", "María", "Ana", "Luis", "Pedro", "Laura", "Sofía", "Diego", "Valentina" };
        string[] lastNames = { "García", "Rodríguez", "Martínez", "Hernández", "López", "González", "Pérez", "Sánchez", "Ramírez", "Torres" };

        string firstName = firstNames[_random.Next(firstNames.Length)];
        string surName = lastNames[_random.Next(lastNames.Length)];

        return FullName.Create(firstName, surName);
    }

    private Result<Email> GenerateRandomEmail()
    {
        string[] domains = { "gmail.com", "hotmail.com", "outlook.com", "yahoo.com", "empresa.com.co" };
        string name = GenerateRandomString(8).ToLower();
        string domain = domains[_random.Next(domains.Length)];

        string email = $"{name}@{domain}";
        return Email.Create(email);
    }

    private DateTime GenerateRandomDateOfBirth()
    {
        int age = _random.Next(22, 56);
        return DateTime.Now.AddYears(-age).AddDays(_random.Next(365));
    }

    private string GenerateRandomPhoneNumber()
    {
        return $"+57 {_random.Next(300, 350)} {_random.Next(1000000, 9999999)}";
    }

    private ICollection<Experience> GenerateRandomExperiences(int count)
    {
        var experiences = new List<Experience>();

        string[] companies = { "GlobalTech", "InnovaSoft", "TechSolutions", "DataCorp", "SoftwareWorks", "CodeMasters", "DevPro", "SystematicSystems", "ByteWise", "LogicLabs" };
        string[] jobTitles = { "Software Developer", "Project Manager", "DevOps Engineer", "QA Analyst", "Systems Architect", "UX Designer", "Database Administrator", "Product Owner", "Technical Lead", "Full Stack Developer" };
        string[] currencies = { "USD", "COP", "EUR" };

        DateTime endDate = DateTime.Now.AddMonths(-1);

        for (int i = 0; i < count; i++)
        {
            int durationMonths = _random.Next(12, 48);
            DateTime startDate = endDate.AddMonths(-durationMonths);
            DateTime? currentEndDate = i == 0 ? null : endDate;
            decimal amount = _random.Next(2000, 10000) + (decimal)Math.Round(_random.NextDouble(), 2);
            string currency = currencies[_random.Next(currencies.Length)];

            var periodResult = currentEndDate.HasValue
                ? Period.Create(startDate, currentEndDate.Value)
                : Result<Period>.Success(Period.Create(startDate, DateTime.MaxValue).Value);

            var salaryResult = Salary.Create(amount, currency);

            experiences.Add(new Experience
            {
                Id = Guid.NewGuid(),
                Company = companies[_random.Next(companies.Length)],
                Job = jobTitles[_random.Next(jobTitles.Length)],
                Description = $"Worked on various {jobTitles[_random.Next(jobTitles.Length)].ToLower()} projects using key technologies and methodologies.",
                Period = periodResult.Value,
                Salary = salaryResult.Value
            });

            endDate = startDate.AddDays(-15);
        }

        return experiences;
    }

    private string GenerateRandomString(int length)
    {
        const string chars = "abcdefghijklmnopqrstuvwxyz";
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[_random.Next(s.Length)]).ToArray());
    }
}
