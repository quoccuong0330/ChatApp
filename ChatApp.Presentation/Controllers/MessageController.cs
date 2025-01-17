using System.Security.Claims;
using AutoMapper;
using ChatApp.Application.Commands.Message;
using ChatApp.Application.Dtos.Requests.Message;
using ChatApp.Application.Dtos.Responses.Message;
using ChatApp.Core.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Presentation.Controllers;

[ApiController]
[Route("api/[action]")]
public class MessageController : ControllerBase {
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public MessageController(ISender sender, IMapper mapper) {
        _sender = sender;
        _mapper = mapper;
    }

    [HttpPost("create-message")]
    [Authorize]
    public async Task<IActionResult> CreateMessage(CreateMessageDto? messageDto) {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userIdClaim))
            return Unauthorized("User ID not found in token.");
        var userId = Guid.Parse(userIdClaim);
        
        var message = await _sender.Send(new CreateCommand(messageDto,userId));
        return message.Flag is false ? BadRequest(message.Message) :Ok(_mapper.Map<MessageResponseDto>(message.MessageModel));
    }
}