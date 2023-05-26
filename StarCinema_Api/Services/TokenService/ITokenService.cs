using StarCinema_Api.Data.Entities;

namespace StarCinema_Api.Services.TokenService
{
    /*
        Account : HungTD34
        Description : Interface of TokenService
        Create : 2023/05/04
     */
    public interface ITokenService
    {
        //Create new token HungTD34
        string CreateToken(User user);
    }
}
