using Microsoft.AspNetCore.Identity;

namespace StudentPortal1.Repositories
{
    public interface ITokenRepository
    {
        string CreateJWTToken(IdentityUser user, List<string> roles);
    }
}
