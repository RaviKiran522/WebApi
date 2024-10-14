using Microsoft.AspNetCore.Identity;

namespace NZwalks.Repositories
{
    public interface ITokenRepository
    {
        string CreateJWTToken(IdentityUser user, List<string> roles);
    }
}
