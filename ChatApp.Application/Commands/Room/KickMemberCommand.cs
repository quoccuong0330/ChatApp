using ChatApp.Application.Dtos.Responses.Room;
using ChatApp.Core.Interfaces;
using MediatR;

namespace ChatApp.Application.Commands.Room;

public record KickMemberCommand(Guid IdRoom, Guid IdMember, Guid IdUser) : IRequest<RecordRoomResponseDto>;

public class KickMemberCommandHandler(IRoomRepository roomRepository)
    : IRequestHandler<KickMemberCommand, RecordRoomResponseDto> {
    public async Task<RecordRoomResponseDto> Handle(KickMemberCommand request, CancellationToken cancellationToken) {
        return await roomRepository.KickMember(request.IdRoom, request.IdMember, request.IdUser);
    }
}