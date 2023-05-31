using MailKit.Security;
using MimeKit.Text;
using MailKit.Net.Smtp;
using MimeKit;
using StarCinema_Api.DTOs;
using StarCinema_Api.Repositories.UserRepository;
using StarCinema_Api.Services.TokenService;

namespace StarCinema_Api.Services.EmailService
{
    
    /// <summary>
    ///    Account : HungTD34
    ///    Description : This class to send email to user
    ///    Create : 2023/05/23
    /// </summary>
     
    public class EmailService : IEmailService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;

        /// <summary>
        /// Get Email config in appsettings.json HungTD34
        /// </summary>
        private readonly IConfiguration _config;
        public EmailService(IUserRepository userRepository, ITokenService tokenService, IConfiguration config)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
            _config = config;
        }
        /// <summary>
        /// Send new email to user HungTD34
        /// </summary>
        /// <param name="to"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        public ResponseDTO SendEmail(string to, string subject, string body)
        {
            try
            {
                //Create email HungTD34
                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse(_config.GetSection("Email:From").Value));
                email.To.Add(MailboxAddress.Parse(to));
                email.Subject = subject;
                email.Body = new TextPart(TextFormat.Text) { Text = body };

                using var smtp = new SmtpClient();


                //Config smtp to send email HungTD34
                smtp.Connect(_config.GetSection("Email:Host").Value, int.Parse(_config.GetSection("Email:Port").Value), SecureSocketOptions.StartTls);
                smtp.Authenticate(_config.GetSection("Email:From").Value, _config.GetSection("Email:Password").Value);
                smtp.Send(email);
                smtp.Disconnect(true);
                return new ResponseDTO()
                {
                    code = 200,
                    message = "Success! Please check your email",
                    data = null
                };
            }
            catch (Exception e)
            {
                return new ResponseDTO()
                {
                    code = 400,
                    message = "Faile. " + e.Message,
                    data = null
                };
            }
        }
    }
}
