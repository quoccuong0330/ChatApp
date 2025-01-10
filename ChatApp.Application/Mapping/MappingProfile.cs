using AutoMapper;
using ChatApp.Application.Dtos.Requests.User;
using ChatApp.Application.Dtos.Responses.User;
using ChatApp.Core.Models;

namespace ChatApp.Application.Mapping;

public class MappingProfile : Profile {
   public MappingProfile() {
      CreateMap<RegisterUserDto, UserModel>();
      CreateMap<UserModel, ResponseUserDto>();
   }
}