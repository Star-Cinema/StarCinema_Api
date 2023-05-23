namespace StarCinema_Api.DTOs
{
    public class StatisticalDTO
    {
        public double? TotalRevenueServicesByMonth { get; set; }

        public double? TotalRevenueTicketsByMonth { get; set; }

        public double? TotalRevenueByMonth { get; set; }

        public double? TotalRevenue { get; set; }

        public double? PercentRevenueGrowthServices { get; set; }
        public double? PercentRevenueGrowthTicket { get; set; }
        public double? PercentRevenueGrowthPrice { get; set; }
        
    }
}
