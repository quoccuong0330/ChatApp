using ChatApp.Core.Models;

namespace ChatApp.Core.Interfaces;

public interface IRoomRepository {
    public Task<RoomModel> CreateRoom(ICollection<Guid> members, Guid createUserId);
    public Task<ICollection<RoomModel>> GetListRoomByUserId(Guid userId);
    public Task<RoomModel?> GetDetailRoom(Guid roomId);
    public Task<ICollection<RoomModel>> FindRoomByName(string name);
    public Task<RoomModel?> UpdateAvatar(Guid idRoom, string avatar);
    public Task<RoomModel?> UpdateName(Guid idRoom, string name);
    public Task<bool> DeleteRoom(Guid roomId);

    public Task<bool> LeaveRoom(Guid idRoom, Guid idUser);
    public Task<bool> KickMember(Guid idRoom, Guid idUser);
    public Task<ICollection<MessageModel>> FindMessageByRoomId(Guid idRoom);


}