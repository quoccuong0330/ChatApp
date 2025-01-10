using ChatApp.Core.Interfaces;
using ChatApp.Core.Models;
using MediatR;

namespace ChatApp.Application.Commands.User;

public record UpdateAvatarCommand(Guid Id, string avatar) : IRequest<UserModel?>;

public class UpdateAvatarCommandHandler(IUserRepository userRepository)
    : IRequestHandler<UpdateAvatarCommand, UserModel?> {
    public async Task<UserModel?> Handle(UpdateAvatarCommand request, CancellationToken cancellationToken) {
        return await userRepository.UpdateAvatar(request.Id, request.avatar);
    }
}