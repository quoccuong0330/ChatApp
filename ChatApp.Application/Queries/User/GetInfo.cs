using ChatApp.Core.Interfaces;
using ChatApp.Core.Models;
using MediatR;

namespace ChatApp.Application.Queries.User;

public record GetInfoQuery(Guid id) : IRequest<UserModel>;

public  class GetInfoQueryHandler(IUserRepository userRepository) : IRequestHandler<GetInfoQuery, UserModel> {
    public async Task<UserModel?> Handle(GetInfoQuery request, CancellationToken cancellationToken) {
        return await userRepository.GetUser(request.id);
    }
}