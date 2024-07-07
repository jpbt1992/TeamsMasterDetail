using TeamsMasterDetail.Domain.Entities;
using TeamsMasterDetail.Infrastructure.Helpers;

namespace TeamsMasterDetail.Infrastructure.Repositories.Interfaces
{
    public interface IMemberRepository : IEntityRepository<Member>
    {
        #region Queries
        Task LoadMembersByTeam(Team team);
        #endregion
    }
}
