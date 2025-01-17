using ChatApp.Application.Dtos.Responses.Room;
using ChatApp.Core.Interfaces;
using ChatApp.Core.Models;
using MediatR;

namespace ChatApp.Application.Queries.Room;

public record FindRoomQuery(string Name) : IRequest<ListRoomRecordDto>;

public class FindRoomQueryHandler(IRoomRepository roomRepository) : IRequestHandler<FindRoomQuery, ListRoomRecordDto> {
    public async Task<ListRoomRecordDto> Handle(FindRoomQuery request, CancellationToken cancellationToken) {
        return await roomRepository.FindRoomByName(request.Name);
    }
}