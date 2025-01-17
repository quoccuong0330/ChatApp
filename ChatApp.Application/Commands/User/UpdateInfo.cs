using ChatApp.Application.Dtos.Responses.User;
using ChatApp.Core.Interfaces;
using ChatApp.Core.Models;
using MediatR;

namespace ChatApp.Application.Commands.User;

public record  UpdateInfoCommand(UpdateUserDto.UpdateInfoDto UpdateUserDto, Guid Id) : IRequest<RecordUserResponseDto>;

public class UpdateInfoCommandHandler(IUserRepository userRepository) : IRequestHandler<UpdateInfoCommand, RecordUserResponseDto> {
    public async Task<RecordUserResponseDto> Handle(UpdateInfoCommand request, CancellationToken cancellationToken) {
        return await userRepository.UpdateUser(request.UpdateUserDto, request.Id);
    }
}