using StarCinema_Api.DTOs;

namespace StarCinema_Api.Services.EmailService
{
    /// <summary>
    ///    Account : HungTD34
    ///    Description : Interface of EmailService
    ///    Create : 2023/05/04
    /// </summary>
     
    public interface IEmailService
    {
        /// <summary>
        /// Send new email to user HungTD34
        /// </summary>
        /// <param name="to"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        ResponseDTO SendEmail(string to, string subject, string body);
        //ResponseDTO VerifyEmail(string email, string token);
    }
}
