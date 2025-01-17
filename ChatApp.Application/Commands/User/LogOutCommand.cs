using ChatApp.Application.Dtos.Responses.User;
using ChatApp.Core.Interfaces;
using MediatR;

namespace ChatApp.Application.Commands.User;

public record LogOutCommand(Guid IdUser,string RefreshTokenFromRequest) : IRequest<RecordUserResponseDto>;

public class LogOutCommandHandler(IUserRepository userRepository) : IRequestHandler<LogOutCommand,RecordUserResponseDto> {
    public async Task<RecordUserResponseDto> Handle(LogOutCommand request, CancellationToken cancellationToken) {
        return await userRepository.LogOut(request.IdUser, request.RefreshTokenFromRequest);
    }
}