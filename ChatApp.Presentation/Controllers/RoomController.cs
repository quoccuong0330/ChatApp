using System.Security.Claims;
using AutoMapper;
using ChatApp.Application.Commands.Room;
using ChatApp.Application.Dtos.Requests.Room;
using ChatApp.Application.Dtos.Responses.Message;
using ChatApp.Application.Dtos.Responses.Room;
using ChatApp.Application.Extensions;
using ChatApp.Application.Queries.Room;
using ChatApp.Core.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoomController : ControllerBase {
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public RoomController(ISender sender, IMapper mapper) {
        _sender = sender;
        _mapper = mapper;
    }

    [HttpPost("create-room")]
    [Authorize]
    public async Task<IActionResult> CreateRoom([FromBody] CreateRoomDto createRoomDto) {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var ownerIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(ownerIdString)) return Unauthorized("Not found id in access token");
        var ownerId = Guid.Parse(ownerIdString);
        
        createRoomDto.members.Add(ownerId);
        var room = await _sender.Send(new CreateCommand(createRoomDto, ownerId));
        return Ok(_mapper.Map<ResponseRoomDto>(room.RoomModel));
    }

    [HttpGet("list-room")]
    [Authorize]
    public async Task<IActionResult> FindListRoomOfUser() {

        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId)) return Unauthorized("Not found id from access token");
        var id = Guid.Parse(userId);
        
        var res = await _sender.Send(new GetListRoomQuery(id));
        var listDto = res.Rooms.Select(l => _mapper.Map<ListResponseDto>(l)).ToList();
       
        return Ok(listDto);
    }

    [HttpGet("get-detail/{id}")]
    [Authorize]
    public async Task<IActionResult> GetDetail([FromRoute] Guid id) {
        
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId)) return Unauthorized("Not found id from access token");
        var idUserGuid = Guid.Parse(userId);

        var room = await _sender.Send(new GetDetailRoom(id, idUserGuid));
        return room.Flag is false ? NotFound(room.Message) 
            : Ok(_mapper.Map<DetailRoomDto>(room.RoomModel));
    }
    
    
    [HttpGet("find-by-name/{name}")]
    [Authorize]
    public async Task<IActionResult> GetListByName([FromRoute] string name) {
        var rooms = await _sender.Send(new FindRoomQuery(name));
        var listDto = rooms.Rooms.Select(l => _mapper.Map<ListResponseDto>(l)).ToList();
        return Ok(listDto);
    }
    
    [HttpPut("add-member/{idRoom}")]
    [Authorize]
    public async Task<IActionResult> AddMember([FromRoute] Guid idRoom, [FromBody] Guid idMember) {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId)) return Unauthorized("Not found id from access token");
        var idUserGuid = Guid.Parse(userId);
        
        var room = await _sender.Send(new AddMemberCommand(idRoom,idMember, idUserGuid));
        return room.Flag is false ? Ok(room.Message):BadRequest(room.Message) ;
    }
    
    [HttpPut("update-avatar/{id}")]
    [Authorize]
    public async Task<IActionResult> UpdateAvatar([FromRoute] Guid id, [FromBody] string avatar) {
        
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId)) return Unauthorized("Not found id from access token");
        var idUserGuid = Guid.Parse(userId);
        
        var room = await _sender.Send(new UpdateRoomAvatarCommand(id, avatar, idUserGuid));
        Console.WriteLine(room.Flag);
        return room.Flag is false ? NotFound( ) : Ok(_mapper.Map<ResponseRoomDto>(room.RoomModel));
    }
    
    [HttpPut("update-name/{id}")]
    [Authorize]
    public async Task<IActionResult> UpdateName([FromRoute] Guid id, [FromBody] string name) {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId)) return Unauthorized("Not found id from access token");
        var idUserGuid = Guid.Parse(userId);
        
        var room = await _sender.Send(new UpdateRoomNameCommand(id, name,idUserGuid));
        return room.Flag is false ? NotFound( ) : Ok(_mapper.Map<ResponseRoomDto>(room.RoomModel));
    }
    
    [HttpDelete("delete-room/{id}")]
    [Authorize]
    public async Task<IActionResult> DeleteRoom([FromRoute] Guid id) {
        
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId)) return Unauthorized("Not found id from access token");
        var idUserGuid = Guid.Parse(userId);
        
        var room = await _sender.Send(new DeleteRoomCommand(id, idUserGuid));
        return room.Flag is false ? BadRequest(room.Message) : Ok(room.Message)  ;
    }
    
    [HttpDelete("leave-room/{id}")]
    [Authorize]
    public async Task<IActionResult> LeaveRoom([FromRoute] Guid id) {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId)) return Unauthorized("Not found id from access token");
        var idUserGuid = Guid.Parse(userId);
        
        var room = await _sender.Send(new LeaveChatCommand(id, idUserGuid));
        return room.Flag is false ? Ok(room.Message):BadRequest(room.Message) ;
    }
    
    [HttpDelete("kick-member/{idRoom}")]
    [Authorize]
    public async Task<IActionResult> KickMember([FromRoute] Guid idRoom, [FromBody] Guid idMember) {
        
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId)) return Unauthorized("Not found id from access token");
        var idUserGuid = Guid.Parse(userId);
        
        var room = await _sender.Send(new KickMemberCommand(idRoom,idMember, idUserGuid));
        return room.Flag is false ? Ok(room.Message):BadRequest(room.Message) ;
    }
    
   
}