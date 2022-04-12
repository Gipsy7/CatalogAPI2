using CatalogAPI2.Models;

namespace CatalogAPI2.Services
{
    public interface ITokenService
    {
        string GenerateToken(string key, string issuer, string audience, UserModel user);
    }
}
