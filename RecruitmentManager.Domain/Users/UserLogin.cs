using Microsoft.AspNetCore.Identity;

namespace RecruitmentManager.Domain.Users;

public class UserLogin : IdentityUserLogin<Guid>
{
    public DateTime LoginDate { get; set; }

    public virtual User User { get; set; }
}