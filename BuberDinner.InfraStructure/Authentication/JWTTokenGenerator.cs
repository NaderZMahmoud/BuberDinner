using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BuberDinner.Application.Common.Interfaces.Authentication;
using Microsoft.IdentityModel.Tokens;

namespace BuberDinner.InfraStructure.Authentication;

public class JWTTokenGenerator : IJWTTokenGenerator
{
    public string GenerateToken(Guid userId, string firstName, string lastName)
    {
        // Implementation for generating JWT token
        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes("your-secret-key")),
            SecurityAlgorithms.HmacSha256);
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
            new Claim(JwtRegisteredClaimNames.GivenName, firstName),
            new Claim(JwtRegisteredClaimNames.FamilyName, lastName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var securityToken = new JwtSecurityToken(issuer: "BuberDinner",expires:DateTime.Now.AddDays(1), claims: claims, signingCredentials: signingCredentials);
        return new JwtSecurityTokenHandler().WriteToken(securityToken);
    }
}