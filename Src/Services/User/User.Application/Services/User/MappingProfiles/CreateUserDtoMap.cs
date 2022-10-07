using AutoMapper;
using User.Application.Core.User.Commands;
using User.Application.Core.User.Dtos;

namespace User.Application.Services.User.MappingProfiles;

public class CreateUserDtoMap : Profile
{
    public CreateUserDtoMap()
    {
        CreateMap<CreateUserDto, CreateUserCommand>();
    }
}