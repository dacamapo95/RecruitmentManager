using Microsoft.AspNetCore.Identity;
using RecruitmentManager.Domain.Primitives;

namespace RecruitmentManager.Domain.Users;

public class User : IdentityUser<Guid>, IEntity<Guid>
{
 
    public string Names { get; set; }

    public string LastNames { get; set; }

    public string? Password { get; set; }


    public virtual ICollection<UserRole> UserRoles { get; set; }

    public virtual ICollection<UserLogin> Logins { get; set; }

    public virtual ICollection<UserToken> Tokens { get; set; }

    public bool IsValidRefreshToken(string refreshToken)
    {
        var token = Tokens.FirstOrDefault(t => t.Token == refreshToken && t.ExpiryTime.HasValue);
        return token != null && DateTime.UtcNow < token.ExpiryTime.Value;
    }

    public void SetRefreshToken(string refresh, DateTime expiryTime)
    {
        var token = Tokens.FirstOrDefault();
        if (token == null)
        {
            Tokens.Add(new UserToken(refresh, expiryTime));
        }
        else
        {
            token.Token = refresh;
            token.ExpiryTime = expiryTime;
        }
    }
}
