using AutoMapper;
using ChatApp.Application.Commands.User;
using ChatApp.Application.Dtos.Requests.User;
using ChatApp.Application.Dtos.Responses.User;
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

    [HttpPost()]
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
    
}