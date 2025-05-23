using Microsoft.AspNetCore.Identity;

namespace RecruitmentManager.Domain.Users;

public class UserToken : IdentityUserToken<Guid>
{
    public string? Token { get; set; }

    public DateTime? ExpiryTime { get; set; }

    public virtual User User { get; set; }

    public UserToken(string? refreshToken, DateTime? expirationDate)
    {
        LoginProvider = "RecruitmentManager";
        Name = "RecruitmentManager";
        Token = refreshToken;
        ExpiryTime = expirationDate;
    }

    public UserToken()
    {

    }
}