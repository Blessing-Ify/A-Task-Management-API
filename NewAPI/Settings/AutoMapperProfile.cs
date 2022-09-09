using AutoMapper;
using NewAPI.DTOs;
using NewAPI.Model;

namespace NewAPI.Settings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //       <source, Destination>
            CreateMap<UserToAddDto, User>()
                .ForMember(Dest => Dest.UserName, opt => opt.MapFrom(src => src.Email)); //the additional setting is because we need username for identity
        }
    }
}
