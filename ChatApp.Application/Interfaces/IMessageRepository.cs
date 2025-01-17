using ChatApp.Application.Dtos.Requests.Message;
using ChatApp.Application.Dtos.Responses.Message;
using ChatApp.Core.Models;

namespace ChatApp.Core.Interfaces;

public interface IMessageRepository {
    public Task<MessageResponseRecord> CreateMessage(CreateMessageDto messageDto, Guid id);
   

}