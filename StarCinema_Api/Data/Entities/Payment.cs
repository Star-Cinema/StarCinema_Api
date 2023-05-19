using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace StarCinema_Api.Data.Entities
{
    public class Payment
    {
        [Key]
        public long Id { get; set; }
        [ForeignKey("Bookings")]
        public int bookingId { get; set; }
        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Amount must be greater than or equal to 0")]
        public double Amount { get; set; }

        public DateTime CreatedDate { get; set; }

        public string ModeOfPayment { get; set; }
        public virtual Bookings Bookings { get; set; }

    }
}