using StarCinema_Api.Data.Entities;

namespace StarCinema_Api.Services.TokenService
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}
