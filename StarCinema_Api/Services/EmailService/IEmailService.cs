using StarCinema_Api.DTOs;

namespace StarCinema_Api.Services.EmailService
{
    public interface IEmailService
    {
        ResponseDTO SendEmail(string to, string subject, string body);
        ResponseDTO VerifyEmail(string email, string token);
    }
}
