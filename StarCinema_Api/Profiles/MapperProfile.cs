using AutoMapper;
using StarCinema_Api.Data.Entities;
using StarCinema_Api.DTOs;

namespace StarCinema_Api.Profiles
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<ScheduleDTO, Schedules>();
            CreateMap<Schedules, ScheduleDTO>();
        }
    }
}
