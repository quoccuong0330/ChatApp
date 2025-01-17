using ChatApp.Application.Dtos.Requests.Message;
using ChatApp.Application.Dtos.Responses.Message;
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


    public async Task<MessageResponseRecord> CreateMessage(CreateMessageDto messageDto, Guid userId) {
        var isUserExist = await _context.RoomMembers.AnyAsync(x => x.UserId.Equals(userId) && x.RoomId.Equals(messageDto.RoomId));
        if ( isUserExist is false) 
            return new MessageResponseRecord(true,new MessageModel(),"Sent successful");

        var id = Guid.NewGuid();
        var messageModel = new MessageModel {
            Id = id,
            Content = messageDto.Content,
            CreatedAt = DateTime.UtcNow,
            MessageType = messageDto.MessageType,
            UserIdCreate = userId,
            RoomId = messageDto.RoomId
        };
        
        await _context.Messages.AddAsync(messageModel);
        var room = await _context.Rooms.FirstOrDefaultAsync(x => x.Id.Equals(messageModel.RoomId));
        room.Messages.Add(messageModel);
        await _context.SaveChangesAsync();
        var message = await _context.Messages
            .Include(x => x.User)
            .FirstOrDefaultAsync(x => x.Id.Equals(id));
        
        return new MessageResponseRecord(true,message,"Sent successful");
    }
}