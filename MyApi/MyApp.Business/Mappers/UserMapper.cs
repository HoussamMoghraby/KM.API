using AutoMapper;
using MyApp.Data.DTOs;
using MyApp.Data.Models;
using MyApp.Data.Payloads;

namespace MyApp.Business.Mappers
{
    public class UserMapper : Profile
    {
        public UserMapper()
        {
            CreateMap<AddUserPayload, User>()
                .ForMember(userEntity => userEntity.Country,
                options => options.Ignore()
                )
                .ForMember(userEntity => userEntity.Role,
                options => options.Ignore()
                ).ReverseMap();

            CreateMap<UpdateUserPayload, User>().ReverseMap();

            CreateMap<User, UserDTO>()
                .ForMember(userDTO => userDTO.Country,
                options => options.MapFrom(src => src.Country.Name)
                ); //.ReverseMap() for bidirectional mapping
        }
    }
}
