using TeamsMasterDetail.Domain.Context;
using TeamsMasterDetail.Domain.Entities;
using TeamsMasterDetail.Infrastructure.Repositories.Interfaces;

namespace TeamsMasterDetail.Infrastructure.Repositories.Implementations
{
    public class TeamRepository(IUnitOfWorkRepository<AppDbContext> unitOfWorkRepository)
        : EntityRepository<Team>(unitOfWorkRepository), ITeamRepository
    { }
}