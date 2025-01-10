using ChatApp.Core.Interfaces;
using ChatApp.Core.Models;
using MediatR;

namespace ChatApp.Application.Commands.User;

public record  class RegisterCommand(UserModel userModel) : IRequest<UserModel>;

public class RegisterCommandHandler(IUserRepository userRepository) : IRequestHandler<RegisterCommand, UserModel> {
    public async Task<UserModel> Handle(RegisterCommand request, CancellationToken cancellationToken) {
        return await userRepository.Register(request.userModel);
    }
}