using ChatApp.Core.Interfaces;
using ChatApp.Core.Models;
using ChatApp.Infrastucture.Data;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Infrastucture.Repositories;

public class MessageRepository : IMessageRepository {
    private readonly ApplicationDbContext _context;

    public MessageRepository(ApplicationDbContext context) {
        _context = context;
    }


    public async Task<MessageModel?> CreateMessage(MessageModel? messageModel) {
        var isRoomExist = await _context.Rooms.AnyAsync(x => x.Id.Equals(messageModel.RoomId));
        var isUserExist = await _context.User.AnyAsync(x => x.Id.Equals(messageModel.UserIdCreate));
        if (isRoomExist is false || isUserExist is false) return null;
        await _context.Messages.AddAsync(messageModel);
        var room = await _context.Rooms.FirstOrDefaultAsync(x => x.Id.Equals(messageModel.RoomId));
        room.Messages.Add(messageModel);
        await _context.SaveChangesAsync();
        return messageModel;
    }
}