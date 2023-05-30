using StarCinema_Api.DTOs;

namespace StarCinema_Api.Services.EmailService
{
    
    ///    Account : HungTD34
    ///    Description : Interface of EmailService
    ///    Create : 2023/05/04
     
    public interface IEmailService
    {
        //Send new email to user HungTD34
        ResponseDTO SendEmail(string to, string subject, string body);
        //ResponseDTO VerifyEmail(string email, string token);
    }
}
