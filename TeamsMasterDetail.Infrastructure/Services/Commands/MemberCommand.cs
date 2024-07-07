using MediatR;
using TeamsMasterDetail.Domain.Entities;
using TeamsMasterDetail.Infrastructure.Helpers;
using TeamsMasterDetail.Infrastructure.Repositories.Interfaces;

namespace TeamsMasterDetail.Infrastructure.Services.Commands
{
    public record MemberCommand(Member Member, CommandMode CommandMode) : IRequest<int>;

    public class MemberHandlerCommand(IMemberRepository memberRepository) : IRequestHandler<MemberCommand, int>
    {
        public Task<int> Handle(MemberCommand request, CancellationToken cancellationToken)
            => memberRepository.CommandAsync(request.Member, request.CommandMode, cancellationToken);
    }
}
