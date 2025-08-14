using TaskTrackerAPI.Domain.Models;

namespace TaskTrackerAPI.DataAccess.Interface;

public interface ITokenService
{
    string GenerateAccessToken(Guid userId, string userName , Role role );
    
    string GenerateRefreshToken();
}