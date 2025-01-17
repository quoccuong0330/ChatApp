using ChatApp.Application.Dtos.Responses.User;
using ChatApp.Core.Interfaces;
using ChatApp.Core.Models;
using MediatR;

namespace ChatApp.Application.Queries.User;

public record GetInfoQuery(Guid Id) : IRequest<RecordUserResponseDto>;

public  class GetInfoQueryHandler(IUserRepository userRepository) : IRequestHandler<GetInfoQuery, RecordUserResponseDto> {
    public async Task<RecordUserResponseDto> Handle(GetInfoQuery request, CancellationToken cancellationToken) {
        return await userRepository.GetUser(request.Id);
    }
}