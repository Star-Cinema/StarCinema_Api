﻿using System.ComponentModel.DataAnnotations;

namespace StarCinema_Api.Data.Entities
{
    public class Bookings
    {
        [Key]
        public int Id { get; set; }

        [Required] 
        public int UserId { get; set; }

        [Required]
        public DateTime CreateAt { get; set; }
        public string Status { get; set; }
        public string UrlPayment { get; set; }

        public virtual Payment Payment { get; set; }

        public bool IsDelete { get; set; } = false;

        public virtual User User { get; set; }
        public virtual List<BookingDetail> BookingDetails { get; set; }
        public virtual List<Services> Services { get; set; }
    }
}
