using StarCinema_Api.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace StarCinema_Api.DTOs
{
    public class BookingDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        public DateTime? CreateAt { get; set; }

        public List<BookingDetail>? BookingDetails { get; set; }
        public List<Data.Entities.Services>? Services { get; set; }


    }
}
