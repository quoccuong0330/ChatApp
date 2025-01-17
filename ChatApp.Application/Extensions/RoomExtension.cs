using ChatApp.Application.Dtos.Responses.Message;
using ChatApp.Application.Dtos.Responses.Room;
using ChatApp.Application.Dtos.Responses.User;
using ChatApp.Core.Models;

namespace ChatApp.Application.Extensions;

public static class RoomExtension {
    // public static DetailRoomDto ToDetailRoomDto(this RoomModel roomModel) {
    //     return new DetailRoomDto
    //     {
    //         Id = roomModel.Id,
    //         Name = roomModel.Name,
    //         Avatar = roomModel.Avatar,
    //         Message = roomModel.Messages.OrderByDescending(x => x.CreatedAt).Select(x=>new MessageResponseDto {
    //             Content = x.Content,
    //             IdUser = x.User.Id,
    //             Avatar = x.User.Avatar,
    //             FirstName = x.User?.FirstName,
    //             LastName = x.User?.LastName,
    //             CreatedAt = x.CreatedAt.ToString(),
    //         }).ToList()
    //     };
    // }

    // public static ResponseRoomDto ToCreateResponseDto(this RoomModel roomModel) {
    //     return new ResponseRoomDto {
    //         Id = roomModel.Id,
    //         Name = roomModel.Name,
    //         Avatar = roomModel.Avatar,
    //         Users = roomModel.RoomMembers.Select(x => new ResponseUserDto {
    //             Id = x.User.Id,
    //             FirstName = x.User?.FirstName,
    //             LastName = x.User?.LastName,
    //             Email = x.User?.Email,
    //             Avatar = x.User?.Avatar,
    //             DateOfBirth = x.User?.DateOfBirth.ToString()
    //         }).ToList()
    //     };
    // }
    
    // public static ListResponseDto ToListResponseDto(this RoomModel roomModel) {
    //     return new ListResponseDto {
    //         Id = roomModel.Id,
    //         Name = roomModel.Name,
    //         Avatar = roomModel.Avatar,
    //         LastMessage = roomModel.Messages.OrderByDescending(x => x.CreatedAt).Select(x=>new MessageResponseDto {
    //             Content = x.Content,
    //             IdUser = x.User.Id,
    //             Avatar = x.User.Avatar,
    //             FirstName = x.User?.FirstName,
    //             LastName = x.User?.LastName,
    //             CreatedAt = x.CreatedAt.ToString(),
    //         }).FirstOrDefault()
    //     };
    // }
}