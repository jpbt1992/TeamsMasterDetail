using MediatR;
using TeamsMasterDetail.Domain.Entities;
using TeamsMasterDetail.Infrastructure.Helpers;
using TeamsMasterDetail.Infrastructure.Repositories.Interfaces;

namespace TeamsMasterDetail.Infrastructure.Services.Commands
{
    public record TeamCommand(Team Team, CommandMode CommandMode) : IRequest<int>;

    public class TeamHandlerCommand(ITeamRepository teamRepository) : IRequestHandler<TeamCommand, int>
    {
        public Task<int> Handle(TeamCommand request, CancellationToken cancellationToken)
            => teamRepository.CommandAsync(request.Team, request.CommandMode, cancellationToken);
    }
}
