using Domain.Entities;

namespace Application.Common.Interfaces;

public interface IJwtProvider
{
    string GenerateJwtToken(User user);
}