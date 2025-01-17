using ChatApp.Application.Dtos.Requests.Room;
using ChatApp.Application.Dtos.Responses.Room;
using ChatApp.Core.Models;

namespace ChatApp.Core.Interfaces;

public interface IRoomRepository {
    public Task<RecordRoomResponseDto> CreateRoom(CreateRoomDto createRoomDto, Guid ownerId);
    public Task<ListRoomRecordDto> GetListRoomByUserId(Guid userId);
    public Task<RecordRoomResponseDto> GetDetailRoom(Guid roomId, Guid idUser);
    public Task<ListRoomRecordDto> FindRoomByName(string name);
    public Task<RecordRoomResponseDto> UpdateAvatar(Guid idRoom, string avatar,  Guid idUser);
    public Task<RecordRoomResponseDto> UpdateName(Guid idRoom, string name,  Guid idUser);
    public Task<RecordRoomResponseDto> DeleteRoom(Guid roomId, Guid idUser);

    public Task<RecordRoomResponseDto> LeaveRoom(Guid idRoom, Guid idUser);
    public Task<RecordRoomResponseDto> KickMember(Guid idRoom, Guid idMember, Guid idUser);
    public Task<RecordRoomResponseDto> AddMember(Guid idRoom, Guid idMember, Guid idUser);
    public Task<ICollection<MessageModel>> FindMessageByRoomId(Guid idRoom);


}