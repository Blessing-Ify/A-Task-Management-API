using AutoMapper;
using NewAPI.DTOs;
using NewAPI.Model;

namespace NewAPI.Settings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
                   //<source, Destination>
            CreateMap<UserDto, User>()
                .ForMember(Dest => Dest.UserName, opt => opt.MapFrom(src => src.Email));
            //the additional setting is because username has no correspondent in the user model
           // CreateMap<User, UserDto>();
        }
    }
}
