using StarCinema_Api.Data.Entities;

namespace StarCinema_Api.Services.TokenService
{
    
    /// <summary>
    ///    Account : HungTD34
    ///    Description : Interface of TokenService
    ///    Create : 2023/05/04
    /// </summary>

    public interface ITokenService
    {
        /// <summary>
        /// Create new token HungTD34
        /// </summary>
        /// <param name="user"></param>
        string CreateToken(User user);
    }
}
