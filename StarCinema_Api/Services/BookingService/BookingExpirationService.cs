using StarCinema_Api.Repositories.BookingRepository;

public class BookingExpirationService : BackgroundService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public BookingExpirationService(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Delay(TimeSpan.FromSeconds(30), stoppingToken); 
            await UpdateExpiredBookings();
        }
    }

    private async Task UpdateExpiredBookings()
    {
        using (var scope = _serviceScopeFactory.CreateScope())
        {
            var bookingRepository = scope.ServiceProvider.GetRequiredService<IBookingRepository>();
            bookingRepository.UpdateBookingsToExpired();
        }
    }
}
