namespace WorkArea.Application.Mapper
{
    using AutoMapper;
    using DTOs;
    using Domain.Entities;
    
    public class DtoMapper:Profile
    {
        public DtoMapper()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, UserSimpleDto>().ReverseMap();
        }
    }
}
