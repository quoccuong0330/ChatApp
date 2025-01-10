using ChatApp.Core.Interfaces;
using ChatApp.Core.Models;
using MediatR;

namespace ChatApp.Application.Commands.User;

public record  UpdateInfoCommand(Guid id, UserModel userModel) : IRequest<UserModel?>;

public class UpdateInfoCommandHandler(IUserRepository userRepository) : IRequestHandler<UpdateInfoCommand, UserModel?> {
    public async Task<UserModel?> Handle(UpdateInfoCommand request, CancellationToken cancellationToken) {
        return await userRepository.UpdateUser(request.id, request.userModel);
    }
}