using AutoMapper;
using ChatApp.Application.Commands.User;
using ChatApp.Application.Dtos.Requests.User;
using ChatApp.Application.Dtos.Responses.User;
using ChatApp.Application.Queries.User;
using ChatApp.Application.Services;
using ChatApp.Core.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Presentation.Controllers;


[Route("/api/[controller]")]
[ApiController]
public class UserController : ControllerBase {
    private readonly ISender _sender;
    private readonly IMapper _mapper;
    public UserController(ISender sender, IMapper mapper) {
        _sender = sender;
        _mapper = mapper;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody]RegisterUserDto userDto) {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        if (!userDto.Password.Equals(userDto.ConfrimPassword)) 
            return BadRequest("Password and confirm password is different");
        var userModel = _mapper.Map<UserModel>(userDto);
        userModel.Password = PasswordHasher.HashPasswordUser(userModel.Password);
        var user = await _sender.Send(new RegisterCommand(userModel));
        var userResponse = _mapper.Map<ResponseUserDto>(user);
        return Ok(userResponse);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetInfoUser([FromRoute] Guid id) {
        var user = await _sender.Send(new GetInfoQuery(id));
        return user is null ? NotFound() : Ok(_mapper.Map<ResponseUserDto>(user));
    }
    
    [HttpPut("update-info/{id:Guid}")]
    public async Task<IActionResult> UpdateInfoUser([FromRoute] Guid id, [FromBody] UpdateUserDto userDto) {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var userModel = _mapper.Map<UserModel>(userDto);
        var user = await _sender.Send(new UpdateInfoCommand(id,userModel));
        return user is null ? NotFound() : Ok(_mapper.Map<ResponseUserDto>(user));
    }
    
    [HttpPut("update-password/{id:Guid}")]
    public async Task<IActionResult> UpdatePasswordUser([FromRoute] Guid id, [FromBody] PasswordDto passwordDto) {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var user = await _sender.Send(new UpdatePasswordCommand(id,passwordDto.oldPassword,passwordDto.newPassword));
        return user is false ? BadRequest() : Ok("Change password successfully");
    }

    
    [HttpPut("update-avatar/{id:Guid}")]
    public async Task<IActionResult> UpdateAvatarUser([FromRoute] Guid id, [FromBody]string avatarUrl) {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var user = await _sender.Send(new UpdateAvatarCommand(id,avatarUrl));
        return user is null ? NotFound() : Ok(_mapper.Map<ResponseUserDto>(user));
    }


    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto) {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var user = await _sender.Send(new LoginQuery(loginDto.email, loginDto.password));
        return user is null ? 
            BadRequest("The email or password is incorrect") : Ok(_mapper.Map<ResponseUserDto>(user));
    } 
    
    
    
}