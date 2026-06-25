using TA.BLL.DTOs.Identity;

namespace TA.BLL.Interfaces;

public interface IJwtProvider
{
    string GenerateAccessToken(Guid userId, string username, string userRole);
    RefreshTokenResult GenerateRefreshToken();
}