using AutoMapper;
using ChatApp.Application.Dtos.Requests.Message;
using ChatApp.Application.Dtos.Requests.User;
using ChatApp.Application.Dtos.Responses.Message;
using ChatApp.Application.Dtos.Responses.Room;
using ChatApp.Application.Dtos.Responses.User;
using ChatApp.Core.Models;

namespace ChatApp.Application.Mapping;

public class MappingProfile : Profile {
   public MappingProfile() {
      CreateMap<RegisterUserDto, UserModel>();
      CreateMap<UpdateUserDto, UserModel>();
      CreateMap<UserModel, ResponseUserDto>()
         .ForMember(dto => dto.DateOfBirth,
            opt =>
               opt.MapFrom(src => src.DateOfBirth.ToString("dd/MM/yyyy")));
      
      CreateMap<CreateMessageDto, MessageModel>().ForMember(messageModel => messageModel.CreatedAt,
         opt =>
         opt.MapFrom(_ => DateTime.UtcNow)); 
      
      //Mapping createRoom with responseRoomDto
      CreateMap<RoomModel, ResponseRoomDto>()
         .ForMember(dest => dest.Users, opt 
            => opt.MapFrom(src => src.RoomMembers));
      
      CreateMap<UserModel,ResponseUserDto>()
         .ForMember(dest => dest.Id, opt 
            => opt.MapFrom(src => src.Id))
         .ForMember(dest => dest.FirstName, opt
            => opt.MapFrom(src => src.FirstName))
         .ForMember(dest => dest.LastName, opt 
            => opt.MapFrom(src => src.LastName))
         .ForMember(dest => dest.Email, opt 
            => opt.MapFrom(src => src.Email))
         .ForMember(dest => dest.Avatar, opt 
            => opt.MapFrom(src => src.Avatar))
         .ForMember(dest => dest.DateOfBirth, opt 
            => opt.MapFrom(src => src.DateOfBirth.ToString()));

      
      CreateMap<RoomMemberModel, ResponseUserDto>()
         .ForMember(dest => dest.Id, opt 
            => opt.MapFrom(src => src.User.Id))
         .ForMember(dest => dest.FirstName, opt
            => opt.MapFrom(src => src.User.FirstName))
         .ForMember(dest => dest.LastName, opt 
            => opt.MapFrom(src => src.User.LastName))
         .ForMember(dest => dest.Email, opt 
            => opt.MapFrom(src => src.User.Email))
         .ForMember(dest => dest.Avatar, opt 
            => opt.MapFrom(src => src.User.Avatar))
         .ForMember(dest => dest.DateOfBirth, opt 
            => opt.MapFrom(src => src.User.DateOfBirth.ToString()));

      //Create Room with ListResponseDto
      CreateMap<RoomModel, ListResponseDto>()
         .ForMember(dest => dest.LastMessage, opt 
            => opt.MapFrom(src => 
            src.Messages.OrderByDescending(m => m.CreatedAt).FirstOrDefault()));

      CreateMap<MessageModel, MessageResponseDto>()
         .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.User.Id))
         .ForMember(dest => dest.Avatar, opt => opt.MapFrom(src => src.User.Avatar))
         .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.User.FirstName))
         .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.User.LastName))
         .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.ToString("dd/M/yyyy HH:mm:ss")));

      CreateMap<RoomModel, DetailRoomDto>();
         
      CreateMap<MessageModel, MessageResponseDto>()
         .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.User.Id))
         .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.User.FirstName))
         .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.User.LastName))
         .ForMember(dest => dest.Avatar, opt => opt.MapFrom(src => src.User.Avatar))
         .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.ToString("dd/M/yyyy HH:mm:ss")));
      
   }
}