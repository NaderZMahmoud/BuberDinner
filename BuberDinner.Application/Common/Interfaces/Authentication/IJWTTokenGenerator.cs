using BuberDinner.Domain.Entities;

namespace BuberDinner.Application.Common.Interfaces.Authentication;

public interface IJWTTokenGenerator
{
    string GenerateToken(User user);
}