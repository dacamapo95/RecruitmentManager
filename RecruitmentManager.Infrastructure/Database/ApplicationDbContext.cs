using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RecruitmentManager.Application.Core.Abstractions;
using RecruitmentManager.Domain.Users;
using RecruitmentManager.Infrastructure.Database.Configurations;
using RecruitmentManager.Infrastructure.Database.Interceptors;
using RecruitmentManager.Domain.Candidates;
using RecruitmentManager.Domain.Countries;

namespace RecruitmentManager.Infrastructure.Database;

public class ApplicationDbContext 
    : IdentityDbContext<User, Role, Guid, IdentityUserClaim<Guid>, UserRole, UserLogin, IdentityRoleClaim<Guid>, UserToken>, 
    IUnitOfWork,
    IApplicationDbContext
{
    private readonly AuditableEntitySaveChangesInterceptor? _auditableInterceptor;

    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options,
        AuditableEntitySaveChangesInterceptor? auditableInterceptor = null
    ) : base(options)
    {
        _auditableInterceptor = auditableInterceptor;
    }

    public DbSet<Candidate> Candidates => Set<Candidate>();
    public DbSet<Address> Addresses => Set<Address>();
    public DbSet<State> States => Set<State>();
    public DbSet<Experience> Experiences => Set<Experience>();
    public DbSet<City> Cities => Set<City>();
    public DbSet<Country> Countries => Set<Country>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        if (_auditableInterceptor is not null)
        {
            optionsBuilder.AddInterceptors(_auditableInterceptor);
        }
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.HasDefaultSchema("RCM");
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(AssemblyReference.Assembly);
        UsersConfiguration.ConfigureUsersContraints(builder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await base.SaveChangesAsync(cancellationToken);
    }
}
