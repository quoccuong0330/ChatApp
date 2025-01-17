using ChatApp.Core.Interfaces;
using MediatR;

namespace ChatApp.Application.Commands.User;

public record UpdatePasswordCommand(Guid Id, string oldPassword, string newPassword) : IRequest<bool>;

public class UpdatePasswordCommandHandler(IUserRepository userRepository)
    : IRequestHandler<UpdatePasswordCommand, bool> {
    public async Task<bool> Handle(UpdatePasswordCommand request, CancellationToken cancellationToken) {
        return await userRepository.UpdatePassword(request.Id, request.oldPassword, request.newPassword);
    }
}