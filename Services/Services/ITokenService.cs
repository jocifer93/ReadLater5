using Microsoft.AspNetCore.Identity;

namespace Services
{
    public interface ITokenService
    {
        string GetToken(IdentityUser user);
    }
}
