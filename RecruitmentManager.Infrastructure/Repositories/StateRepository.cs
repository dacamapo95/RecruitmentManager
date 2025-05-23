using RecruitmentManager.Domain.Candidates;
using RecruitmentManager.Domain.Interfaces;
using RecruitmentManager.Infrastructure.Database;
using RecruitmentManager.Infrastructure.Database.Repositories;

namespace RecruitmentManager.Infrastructure.Repositories;

public class StateRepository(ApplicationDbContext context) : Repository<State, int>(context), IStateRepository
{
}