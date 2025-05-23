namespace RecruitmentManager.Infrastructure.Options;

public class CacheOptions
{
    public int ExpirationTimeInMinutes { get; set; } = 5;
    
    public bool EnableCaching { get; set; } = true;
}