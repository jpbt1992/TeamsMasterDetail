using MediatR;
using TeamsMasterDetail.Domain.Entities;
using TeamsMasterDetail.Infrastructure.Repositories.Interfaces;

namespace TeamsMasterDetail.Infrastructure.Services.Queries
{
    #region ToListAsync
    public record MemberQueryToListAsync() : IRequest<List<Member>>;

    public class MemberQueryHandlerToListAsync(IMemberRepository memberRepository) 
        : IRequestHandler<MemberQueryToListAsync, List<Member>>
    {
        public Task<List<Member>> Handle(MemberQueryToListAsync request, CancellationToken cancellationToken)
            => memberRepository.ToListAsync;
    }
    #endregion

    #region FindAsync
    public record MemberQueryFindAsync(params object?[]? KeyValues) : IRequest<Member?>;

    public class MemberQueryHandlerFindAsync(IMemberRepository memberRepository) : IRequestHandler<MemberQueryFindAsync, Member?>
    {
        public async Task<Member?> Handle(MemberQueryFindAsync request, CancellationToken cancellationToken)
            => await memberRepository.FindAsync(request.KeyValues);
    }
    #endregion

    #region LoadMembersByTeam
    public record MemberQueryLoadMembersByTeam(Team Team) : IRequest;

    public class MemberQueryHandlerLoadMembersByTeam(IMemberRepository memberRepository)
        : IRequestHandler<MemberQueryLoadMembersByTeam>
    {
        public Task Handle(MemberQueryLoadMembersByTeam request, CancellationToken cancellationToken)
            => memberRepository.LoadMembersByTeam(request.Team);
    }
    #endregion
}
