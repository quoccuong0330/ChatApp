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
    
    public async Task<RoomModel> CreateRoom(ICollection<Guid> members, Guid createUserId) {
        foreach (var member in members) {
        Console.WriteLine(member);
            
        }
        var users = await _context.User
            .Where(u => members.Contains(u.Id))
            .ToDictionaryAsync(u => u.Id);
        
        var roomModel = new RoomModel {
            OwnerId = createUserId,
            CreatedAt = DateTime.UtcNow,
            Avatar = "default",
            Name = $"Chat room {new Random().Next()}",
            Owner = await _context.User.FirstOrDefaultAsync(x => x.Id.Equals(createUserId))
        };
        await _context.SaveChangesAsync();
        var room = await _context.Rooms.AddAsync(roomModel);
        
        foreach (var userId in members) {
            Console.WriteLine(userId);
            var roomMember = new RoomMemberModel {
                UserId = userId,
                RoomId = room.Entity.Id,
                JoinedAt = DateTime.UtcNow,
                User = await _context.User.FirstOrDefaultAsync(x => x.Id.Equals(userId))
            };
            await _context.RoomMembers.AddAsync(roomMember);
            await _context.SaveChangesAsync();
        }
        return roomModel;
    }

    public async Task<ICollection<RoomModel>> GetListRoomByUserId(Guid userId) {
        var list = await _context.RoomMembers.Where(x=>x.UserId.Equals(userId)).Select(x=>x.RoomId).ToListAsync();
        var listRoom = new List<RoomModel>();
        
        foreach (var l in list) {
          listRoom.Add(await _context.Rooms
              .Include(x=>x.RoomMembers)
              .Include(x=>x.Messages)
              .FirstOrDefaultAsync(x=>x.Id.Equals(l)));
        }
        return listRoom;
    }

    public async Task<RoomModel?> GetDetailRoom(Guid roomId) {
        var room = await _context.Rooms.Include(x => x.RoomMembers)
            .Include(x=>x.Owner)
            .Include(x => x.Messages).FirstOrDefaultAsync(x => x.Id.Equals(roomId));
       return room;
    }

    public async Task<RoomModel?> UpdateAvatar(Guid idRoom, string avatar) {
        var room = await _context.Rooms.FirstOrDefaultAsync(x => x.Id.Equals(idRoom));
        if (room is null) return null;
        room.Avatar = avatar;
        await _context.SaveChangesAsync();
        return room;
    }

    public async Task<RoomModel?> UpdateName(Guid idRoom, string name) {
        var room = await _context.Rooms.FirstOrDefaultAsync(x => x.Id.Equals(idRoom));
        if (room is null) return null;
        room.Name = name;
        await _context.SaveChangesAsync();
        return room;
    }
    


    public async Task<bool> DeleteRoom(Guid roomId) {
        var room = await _context.Rooms.FirstOrDefaultAsync(x => x.Id.Equals(roomId));
        if (room is null) return false;
         _context.Rooms.Remove(room);
         await _context.SaveChangesAsync();
         return true;
    }

    public async Task<bool> LeaveRoom(Guid idRoom, Guid idUser) {
        var record = await _context.RoomMembers
            .FirstOrDefaultAsync(x => x.RoomId.Equals(idRoom) && x.UserId.Equals(idUser));
        if (record is null) return false;
         _context.RoomMembers.Remove(record);
         await _context.SaveChangesAsync();
         return true;
    }

    public Task<bool> KickMember(Guid idRoom, Guid idUser) {
        throw new NotImplementedException();
    }

    public async Task<ICollection<RoomModel>> FindRoomByName(string name) {
        var listRoom = await _context.Rooms.Where(x => x.Name.Contains(name)).ToListAsync();
        return listRoom;
    }
}