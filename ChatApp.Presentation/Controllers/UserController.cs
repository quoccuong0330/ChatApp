using System.Security.Claims;
using AutoMapper;
using ChatApp.Application.Commands.User;
using ChatApp.Application.Dtos.Requests.User;
using ChatApp.Application.Dtos.Responses.User;
using ChatApp.Application.Queries.User;
using ChatApp.Application.Services;
using ChatApp.Core.Models;
using ChatApp.Infrastucture.SignalR.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Presentation.Controllers;


[Route("/api/[controller]")]
[ApiController]
public class UserController : ControllerBase {
    private readonly ISender _sender;
    private readonly IMapper _mapper;
    private readonly JwtService _jwtService;

    public UserController(ISender sender, IMapper mapper, JwtService jwtService) {
        _sender = sender;
        _mapper = mapper;
        _jwtService = jwtService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody]RegisterUserDto userDto) {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var userModel = _mapper.Map<UserModel>(userDto);
        userModel.Password = PasswordHasher.HashPasswordUser(userModel.Password);
        var user = await _sender.Send(new RegisterCommand(userModel));
        if (user.Flag is false) return BadRequest(user.Message);
        var userResponse = _mapper.Map<ResponseUserDto>(user.UserModel);
        return Ok(userResponse);
    }

    [HttpGet()]
    [Authorize]
    public async Task<IActionResult> GetInfoUser() {
        var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userIdString))
            return Unauthorized("User ID not found in token.");
        var userId = Guid.Parse(userIdString);
        var user = await _sender.Send(new GetInfoQuery(userId));
        return user.Flag is false ? NotFound(user.Message) : Ok(_mapper.Map<ResponseUserDto>(user.UserModel));
    }
    
    [HttpPut("update-info")]
    [Authorize]
    public async Task<IActionResult> UpdateInfoUser([FromBody] UpdateUserDto.UpdateInfoDto userDto) {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        
        var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userIdString))
            return Unauthorized("User ID not found in token.");
        var userId = Guid.Parse(userIdString);
        
        var user = await _sender.Send(new UpdateInfoCommand(userDto,userId));
        return user.Flag is true ? Ok(_mapper.Map<ResponseUserDto>(user.UserModel)) : NotFound(user.Message);
    }
    
    [HttpPut("update-password")]
    [Authorize]
    public async Task<IActionResult> UpdatePasswordUser([FromBody] UpdateUserDto.UpdatePasswordDto passwordDto) {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        
        var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userIdString))
            return Unauthorized("User ID not found in token.");
        var userId = Guid.Parse(userIdString);
        
        var user = await _sender.Send(new UpdatePasswordCommand(passwordDto,userId));
        return user.Flag is false ? BadRequest(user.Message) : Ok("Change password successfully");
    }

    
    [HttpPut("update-avatar")]
    [Authorize]
    public async Task<IActionResult> UpdateAvatarUser( [FromBody]UpdateUserDto.UpdateAvatarDto avatarDto) {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        
        var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userIdString))
            return Unauthorized("User ID not found in token.");
        var userId = Guid.Parse(userIdString);
        
        var user = await _sender.Send(new UpdateAvatarCommand(avatarDto,userId));
        return user.Flag is false ? NotFound() : Ok(_mapper.Map<ResponseUserDto>(user.UserModel));
    }


    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto) {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var user = await _sender.Send(new LoginQuery(loginDto));
        return Ok(user);
    } 
    
    [HttpPost("log-out")]
    [Authorize]
    public async Task<IActionResult> LogOutUser([FromBody] LogoutDto logoutDto) {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        
        var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userIdString))
            return Unauthorized("User ID not found in token.");
        var userId = Guid.Parse(userIdString);
        
        var user = await _sender.Send(new LogOutCommand(userId, logoutDto.RefreshToken));
        return user.Flag is false ? BadRequest(user.Message) : Ok(user.Message);
    }
    
    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken([FromBody] LogoutDto logoutDto) {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var user = await _sender.Send(new RefreshTokenCommand(logoutDto));
        return user.Flag is false ? BadRequest(user.Message) : Ok(user);
    }
    
}