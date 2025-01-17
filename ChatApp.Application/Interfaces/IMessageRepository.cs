using ChatApp.Core.Models;

namespace ChatApp.Core.Interfaces;

public interface IMessageRepository {
    public Task<MessageModel?> CreateMessage(MessageModel? messageModel);
   

}