using TeamsMasterDetail.Domain.Context;
using TeamsMasterDetail.Domain.Entities;
using TeamsMasterDetail.Infrastructure.Repositories.Interfaces;

namespace TeamsMasterDetail.Infrastructure.Repositories.Implementations
{
    public class MemberRepository(IUnitOfWorkRepository<AppDbContext> unitOfWorkRepository)
        : EntityRepository<Member>(unitOfWorkRepository), IMemberRepository
    {
        #region Private Methods
        private readonly IUnitOfWorkRepository<AppDbContext> unitOfWorkRepository = unitOfWorkRepository;
        #endregion

        #region Queries
        public Task LoadMembersByTeam(Team team)
            => unitOfWorkRepository.Context.Entry(team).Collection(team => team.Members).LoadAsync();
        #endregion
    }
}
