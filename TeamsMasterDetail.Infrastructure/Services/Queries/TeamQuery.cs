using MediatR;
using TeamsMasterDetail.Domain.Entities;
using TeamsMasterDetail.Infrastructure.Repositories.Interfaces;

namespace TeamsMasterDetail.Infrastructure.Services.Queries
{
    #region ToListAsync
    public record TeamQueryToListAsync() : IRequest<List<Team>>;

    public class TeamQueryHandlerToListAsync(ITeamRepository teamRepository) : IRequestHandler<TeamQueryToListAsync, List<Team>>
    {
        public Task<List<Team>> Handle(TeamQueryToListAsync request, CancellationToken cancellationToken)
            => teamRepository.ToListAsync;
    }
    #endregion

    #region FindAsync
    public record TeamQueryFindAsync(params object?[]? KeyValues) : IRequest<Team?>;

    public class TeamQueryHandlerFindAsync(ITeamRepository teamRepository) : IRequestHandler<TeamQueryFindAsync, Team?>
    {
        public async Task<Team?> Handle(TeamQueryFindAsync request, CancellationToken cancellationToken)
            => await teamRepository.FindAsync(request.KeyValues);
    }
    #endregion
}
