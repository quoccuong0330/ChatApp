using ChatApp.Application.Dtos.Responses.User;
using ChatApp.Core.Interfaces;
using ChatApp.Core.Models;
using MediatR;

namespace ChatApp.Application.Commands.User;

public record UpdateAvatarCommand(UpdateUserDto.UpdateAvatarDto AvatarDto, Guid Id) : IRequest<RecordUserResponseDto>;

public class UpdateAvatarCommandHandler(IUserRepository userRepository)
    : IRequestHandler<UpdateAvatarCommand, RecordUserResponseDto> {
    public async Task<RecordUserResponseDto> Handle(UpdateAvatarCommand request, CancellationToken cancellationToken) {
        return await userRepository.UpdateAvatar(request.AvatarDto, request.Id);
    }
}