using ChatApp.Application.Dtos.Responses.Room;
using ChatApp.Core.Interfaces;
using ChatApp.Core.Models;
using MediatR;

namespace ChatApp.Application.Queries.Room;

public record GetListRoomQuery(Guid Id) : IRequest<ListRoomRecordDto>;

public  class GetListRoomQueryHandler(IRoomRepository roomRepository) : IRequestHandler<GetListRoomQuery, ListRoomRecordDto> {
    public async Task<ListRoomRecordDto> Handle(GetListRoomQuery request, CancellationToken cancellationToken) {
        return await roomRepository.GetListRoomByUserId(request.Id);
    }
}