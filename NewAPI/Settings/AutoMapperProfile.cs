using AutoMapper;
using NewAPI.DTOs;
using NewAPI.Model;
using TaskManagementAPI.DTOs;

namespace NewAPI.Settings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //<source, Destination>
            CreateMap<UserLoginDto, User>()
                .ForMember(Dest => Dest.UserName, opt => opt.MapFrom(src => src.Email));
            //the additional setting is because username has no correspondent in the user model
            CreateMap<User, UserRegisterDto>().ReverseMap()
                .ForMember(Dest => Dest.UserName, opt => opt.MapFrom(src => src.Email));
            //CreateMap<UserRegisterDto, User>();
        }
    }
}
