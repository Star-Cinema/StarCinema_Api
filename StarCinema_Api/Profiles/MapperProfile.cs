using AutoMapper;
using StarCinema_Api.Data.Entities;
using StarCinema_Api.DTOs;

namespace StarCinema_Api.Profiles
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            //CreateMap<User, MemberDto>()
            //.ForMember(
            //    dest => dest.Age,
            //    options => options.MapFrom(src => src.DateOfBirth.GetAge())
            //);
            //CreateMap<RegisterUserDto, User>();
            //CreateMap<UserCreateDto, User>();
            //CreateMap<User, UserDTO>().ForMember(dest => dest.roleName, opt => opt.MapFrom(src => src.role.Name)); ;
            //CreateMap<UserUpdateDTO, User>();
            CreateMap<User, UserDTO>();
            CreateMap<UserDTO, User>();
            CreateMap<User, CreateUserDTO>();
            CreateMap<CreateUserDTO, User>();
            CreateMap<ScheduleDTO, Schedules>();
            CreateMap<Schedules, ScheduleDTO>();

            //VYVNK1
            CreateMap<FilmDTO, Films>();
            CreateMap<CategoriesDTO, Categories>();
            CreateMap<Categories, CategoriesDTO>();
        }
    }
}
