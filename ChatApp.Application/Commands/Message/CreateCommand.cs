using ChatApp.Application.Dtos.Requests.Message;
using ChatApp.Application.Dtos.Responses.Message;
using ChatApp.Core.Interfaces;
using ChatApp.Core.Models;
using MediatR;

namespace ChatApp.Application.Commands.Message;

public record CreateCommand(CreateMessageDto? CreateMessageDto, Guid UserId) : IRequest<MessageResponseRecord> ;

public class CreateCommandHandler(IMessageRepository messageRepository)
    : IRequestHandler<CreateCommand, MessageResponseRecord> {
    public async Task<MessageResponseRecord> Handle(CreateCommand request, CancellationToken cancellationToken) {
        return await messageRepository.CreateMessage(request.CreateMessageDto, request.UserId);
    }
}