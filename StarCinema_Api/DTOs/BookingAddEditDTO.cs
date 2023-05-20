using StarCinema_Api.Data.Entities;

namespace StarCinema_Api.DTOs
{
    public class BookingAddEditDTO
    {
        public int? UserId { get; set; }

        public int? FilmId { get; set; }

        public int ScheduleId { get; set; }

        public List<int>? ListServiceId { get; set; }
        public List<int>? ListSeatId { get; set; }

    }
}
