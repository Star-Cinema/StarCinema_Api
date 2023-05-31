using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using StarCinema_Api.Data.Entities;

namespace StarCinema_Api.Services.TokenService
{
    
    ///    Account : HungTD34
    ///    Description : This class generates authentication token for user
    ///    Create : 2023/05/04
     
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        //Create new token HungTD34
        public string CreateToken(User user)
        {
            //Add claims in to token HungTD34
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Name, user.Name),
                new Claim("fullname", user.Name),
                new Claim("email", user.Email == null? "" : user.Email),
                new Claim("id", user.Id == null? "" : user.Id.ToString()),
            };

            const int roleAdmin = 1;
            if (user.RoleId == roleAdmin)
                claims.Add(new Claim(ClaimTypes.Role, "admin"));
            else
                claims.Add(new Claim(ClaimTypes.Role, "user"));


            //Config SymmetricSecurityKey HungTD34
            var symmetricKey = new SymmetricSecurityKey
                (Encoding.UTF8.GetBytes(_configuration["TokenKey"]));

            //Config SecurityTokenDescriptor HungTD34
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = new SigningCredentials(
                    symmetricKey, SecurityAlgorithms.HmacSha512Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            //Create token HungTD34
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}