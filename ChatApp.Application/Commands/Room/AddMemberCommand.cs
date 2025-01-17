using ChatApp.Application.Dtos.Responses.Room;
using ChatApp.Core.Interfaces;
using MediatR;

namespace ChatApp.Application.Commands.Room;

public record AddMemberCommand(Guid IdRoom, Guid IdMember, Guid IdUser) : IRequest<RecordRoomResponseDto>;

public class AddMemberCommandHandler(IRoomRepository roomRepository)
    : IRequestHandler<AddMemberCommand, RecordRoomResponseDto> {
    public async Task<RecordRoomResponseDto> Handle(AddMemberCommand request, CancellationToken cancellationToken) {
        return await roomRepository.AddMember(request.IdRoom, request.IdMember, request.IdUser);
    }
}