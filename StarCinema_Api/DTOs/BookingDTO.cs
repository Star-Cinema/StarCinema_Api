using StarCinema_Api.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace StarCinema_Api.DTOs
{
    public class BookingDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        public DateTime? CreateAt { get; set; }

        public string? Status { get; set; }

        public double? TotalPriceTickets { get; set; }

        public double? TotalPriceServices { get; set; }

        public double? TotalPrice { get; set; }

        public string? FilmName { get; set; }

        public string? UserName { get; set; }
        public string? UrlPayment { get; set; }

        public List<BookingDetail>? BookingDetails { get; set; }
        public List<Data.Entities.Services>? Services { get; set; }


    }
}
