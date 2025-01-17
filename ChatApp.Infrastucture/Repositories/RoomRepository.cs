using ChatApp.Application.Dtos.Requests.Room;
using ChatApp.Application.Dtos.Responses.Room;
using ChatApp.Core.Interfaces;
using ChatApp.Core.Models;
using ChatApp.Infrastucture.Data;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Infrastucture.Repositories;

public class RoomRepository : IRoomRepository {
    private readonly ApplicationDbContext _context;
    
    public RoomRepository(ApplicationDbContext context) {
        _context = context;
    }
    
    public async Task<RecordRoomResponseDto> CreateRoom(CreateRoomDto createRoomDto, Guid ownerId) {
        var users = await _context.User
            .Where(u => createRoomDto.members.Contains(u.Id))
            .ToDictionaryAsync(u => u.Id);
        
        var roomModel = new RoomModel {
            OwnerId = ownerId,
            CreatedAt = DateTime.UtcNow,
            Avatar = "default",
            Name = $"Chat room {new Random().Next()}",
            Owner = await _context.User.FirstOrDefaultAsync(x => x.Id.Equals(ownerId))
        };
        await _context.SaveChangesAsync();
        var room = await _context.Rooms.AddAsync(roomModel);
        
        foreach (var userId in createRoomDto.members) {
            var roomMember = new RoomMemberModel {
                UserId = userId,
                RoomId = room.Entity.Id,
                JoinedAt = DateTime.UtcNow,
                User = await _context.User.FirstOrDefaultAsync(x => x.Id.Equals(userId))
            };
            await _context.RoomMembers.AddAsync(roomMember);
            await _context.SaveChangesAsync();
        }
        return new RecordRoomResponseDto(true,room.Entity,"Create new room successful");
    }

    public async Task<ListRoomRecordDto> GetListRoomByUserId(Guid userId) {
        var list = await _context.RoomMembers
            .Where(x=>x.UserId
                .Equals(userId))
            .Select(x=>x.RoomId).ToListAsync();
        var listRoom = new List<RoomModel>();
        
        foreach (var idRoom in list) {
            listRoom.Add(
                await _context.Rooms
                    .Include(x => x.Messages)
                    .ThenInclude(x => x.User)
                    .FirstOrDefaultAsync(x => x.Id.Equals(idRoom))
            );
        }

        return new ListRoomRecordDto(true, listRoom, "Get list room of user successful");
    }

    public async Task<RecordRoomResponseDto> GetDetailRoom(Guid roomId, Guid idUser) {
        var room = await _context.Rooms.Include(x => x.RoomMembers)
            .Include(x=>x.Owner)
            .Include(x => x.Messages)
            .ThenInclude(x=>x.User).FirstOrDefaultAsync(x => x.Id.Equals(roomId));
        if (room is null) return new RecordRoomResponseDto(false,new RoomModel(), "Not found") ;
        var isUserOfRoom = room.RoomMembers.Any(x => x.UserId.Equals(idUser));
        return isUserOfRoom is false ? new RecordRoomResponseDto(false, new RoomModel(), "Unauthorized") 
            : new RecordRoomResponseDto(true, room, "Get room successful");
    }

    public async Task<RecordRoomResponseDto> UpdateAvatar(Guid idRoom, string avatar, Guid idUser) {
        var room = await _context.Rooms
            .Include(x => x.RoomMembers)
            .FirstOrDefaultAsync(x => x.Id.Equals(idRoom));
        if (room is null) return new RecordRoomResponseDto(false, new RoomModel(), "Not found");
        var isUserOfRoom = room.RoomMembers.Any(x => x.UserId.Equals(idUser));
        if (isUserOfRoom is false) return new RecordRoomResponseDto(false, new RoomModel(), "Unauthorized");
        room.Avatar = avatar;
        await _context.SaveChangesAsync();
        return new RecordRoomResponseDto(true, room, "Update avatar successful");
    }

    public async Task<RecordRoomResponseDto> UpdateName(Guid idRoom, string name, Guid idUser) {
        var room = await _context.Rooms
            .Include(x=>x.RoomMembers)
            .FirstOrDefaultAsync(x => x.Id.Equals(idRoom));
        if (room is null)  return new RecordRoomResponseDto(false, new RoomModel(), "Not found");
        var isUserOfRoom = room.RoomMembers.Any(x => x.UserId.Equals(idUser));
        if (isUserOfRoom is false) return new RecordRoomResponseDto(false, new RoomModel(), "Unauthorized");
        room.Name = name;
        await _context.SaveChangesAsync();
        return new RecordRoomResponseDto(true, room, "Update name successful");
    }

    public async Task<RecordRoomResponseDto> DeleteRoom(Guid roomId, Guid idUser) {
        var room = await _context.Rooms
            .FirstOrDefaultAsync(x => x.Id.Equals(roomId));
        if (room is null) return new RecordRoomResponseDto(false,new RoomModel(),"Not found");
        var isOwner = room.OwnerId.Equals(idUser);
        if(isOwner is false) return  new RecordRoomResponseDto(false,new RoomModel(),"Unauthorized");
         _context.Rooms.Remove(room);
         await _context.SaveChangesAsync();
         return new RecordRoomResponseDto(true,room,"Delete room successful");
    }

    public async Task<RecordRoomResponseDto> LeaveRoom(Guid idRoom, Guid idUser) {
        var record = await _context.RoomMembers
            .FirstOrDefaultAsync(x => x.RoomId.Equals(idRoom) && x.UserId.Equals(idUser));
        if (record is null) return  new RecordRoomResponseDto(false,new RoomModel(),"Unauthorized");
        if (await _context.Rooms.AnyAsync(x=>x.OwnerId.Equals(idUser)))
            return  new RecordRoomResponseDto(false,new RoomModel(),"Owner does not allow to leave room");
        _context.RoomMembers.Remove(record);
        await _context.SaveChangesAsync();
        return new RecordRoomResponseDto(true,new RoomModel(),"Leave room successful");
    }

    public async Task<RecordRoomResponseDto> KickMember(Guid idRoom, Guid idMember,Guid idUser) {
        var room = await _context.Rooms
            .Include(x=>x.RoomMembers)
            .FirstOrDefaultAsync(x => x.Id.Equals(idRoom));
        if (room is null) return new RecordRoomResponseDto(false,new RoomModel(),"Not found room");
        var isOwner = room.OwnerId.Equals(idUser);
        if(isOwner is false) return  new RecordRoomResponseDto(false,new RoomModel(),"Unauthorized");
        if(!room.RoomMembers.Any(x=>x.UserId.Equals(idMember)))
            return new RecordRoomResponseDto(false,new RoomModel(),"Member does not in room");
        var roomMember = await _context.RoomMembers
            .FirstOrDefaultAsync(rm => rm.RoomId == idRoom && rm.UserId == idMember);
         _context.RoomMembers.Remove(roomMember!);
         await _context.SaveChangesAsync();
         return new RecordRoomResponseDto(false,new RoomModel(),"Kick member successful");
    }

    public async Task<RecordRoomResponseDto> AddMember(Guid idRoom, Guid idMember, Guid idUser) {
        var room = await _context.Rooms
            .Include(x=>x.RoomMembers)
            .FirstOrDefaultAsync(x => x.Id.Equals(idRoom));
        if (room is null) return new RecordRoomResponseDto(false,new RoomModel(),"Not found room");
        var isOwner = room.OwnerId.Equals(idUser);
        if(isOwner is false) return  new RecordRoomResponseDto(false,new RoomModel(),"Unauthorized");
        if(room.RoomMembers.Any(x=>x.UserId.Equals(idMember)))
            return new RecordRoomResponseDto(false,new RoomModel(),"Member has already in room");
        var roomMember = new RoomMemberModel {
            UserId = idMember,
            RoomId = idRoom,
            JoinedAt = DateTime.UtcNow,
            User = await _context.User.FirstOrDefaultAsync(x => x.Id.Equals(idMember))
        };
        await _context.RoomMembers.AddAsync(roomMember);
        await _context.SaveChangesAsync();
        return new RecordRoomResponseDto(false,new RoomModel(),"Add member successful");
        
    }

    public async Task<ICollection<MessageModel>> FindMessageByRoomId(Guid idRoom) {
        var room = await _context.Rooms.FirstOrDefaultAsync(x => x.Id.Equals(idRoom));
        return room.Messages;
    }

    public async Task<ListRoomRecordDto> FindRoomByName(string name) {
        var listRoom = await _context.Rooms.Include(x=>x.Messages)
            .ThenInclude(x=>x.User).Where(x => x.Name.Contains(name)).ToListAsync();
        return new ListRoomRecordDto(true,listRoom,"Get list room successful");
    }
}