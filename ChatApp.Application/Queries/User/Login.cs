using ChatApp.Core.Interfaces;
using ChatApp.Core.Models;
using MediatR;

namespace ChatApp.Application.Queries.User;

public record LoginQuery(string email, string password) : IRequest<UserModel?>;

public class LoginQueryHandler(IUserRepository userRepository) : IRequestHandler<LoginQuery, UserModel?> {
    public async Task<UserModel?> Handle(LoginQuery request, CancellationToken cancellationToken) {
        return await userRepository.Login(request.email, request.password);
    }
}