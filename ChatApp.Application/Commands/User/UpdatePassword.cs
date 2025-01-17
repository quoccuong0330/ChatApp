using ChatApp.Application.Dtos.Responses.User;
using ChatApp.Core.Interfaces;
using MediatR;

namespace ChatApp.Application.Commands.User;

public record UpdatePasswordCommand(UpdateUserDto.UpdatePasswordDto PasswordDto, Guid Id) : IRequest<RecordUserResponseDto>;

public class UpdatePasswordCommandHandler(IUserRepository userRepository)
    : IRequestHandler<UpdatePasswordCommand, RecordUserResponseDto> {
    public async Task<RecordUserResponseDto> Handle(UpdatePasswordCommand request, CancellationToken cancellationToken) {
        return await userRepository.UpdatePassword(request.PasswordDto, request.Id);
    }
}