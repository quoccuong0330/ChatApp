using ChatApp.Application.Dtos.Requests.User;
using ChatApp.Application.Dtos.Responses.User;
using ChatApp.Core.Interfaces;
using ChatApp.Core.Models;
using MediatR;

namespace ChatApp.Application.Queries.User;

public record LoginQuery(LoginDto LoginDto) : IRequest<LoginResponseDto>;

public class LoginQueryHandler(IUserRepository userRepository) : IRequestHandler<LoginQuery, LoginResponseDto> {
    public async Task<LoginResponseDto> Handle(LoginQuery request, CancellationToken cancellationToken) {
        return await userRepository.Login(request.LoginDto);
    }
}