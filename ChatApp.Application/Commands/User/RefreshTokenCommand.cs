using ChatApp.Application.Dtos.Requests.User;
using ChatApp.Application.Dtos.Responses.User;
using ChatApp.Core.Interfaces;
using MediatR;

namespace ChatApp.Application.Commands.User;

public record RefreshTokenCommand(LogoutDto LogoutDto) : IRequest<LoginResponseDto>;

public class RefreshTokenCommandHandler(IUserRepository userRepository)
    : IRequestHandler<RefreshTokenCommand, LoginResponseDto> {
    public async Task<LoginResponseDto> Handle(RefreshTokenCommand request, CancellationToken cancellationToken) {
        return await userRepository.RefreshToken(request.LogoutDto.RefreshToken);
    }
}