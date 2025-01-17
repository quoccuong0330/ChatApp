using ChatApp.Application.Dtos.Responses.User;
using ChatApp.Core.Interfaces;
using ChatApp.Core.Models;
using MediatR;

namespace ChatApp.Application.Commands.User;

public record   RegisterCommand(UserModel UserModel) : IRequest<RecordUserResponseDto>;

public class RegisterCommandHandler(IUserRepository userRepository) : IRequestHandler<RegisterCommand, RecordUserResponseDto> {
    public async Task<RecordUserResponseDto> Handle(RegisterCommand request, CancellationToken cancellationToken) {
        return await userRepository.Register(request.UserModel);
    }
}