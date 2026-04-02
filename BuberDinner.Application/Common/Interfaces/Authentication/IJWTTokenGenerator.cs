namespace BuberDinner.Application.Common.Interfaces.Authentication;

public interface IJWTTokenGenerator
{
    string GenerateToken(Guid userId, string firstName, string lastName);
}