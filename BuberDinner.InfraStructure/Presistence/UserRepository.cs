using BuberDinner.Application.Common.Interfaces.Presistence;
using BuberDinner.Domain.Entities;

namespace BuberDinner.InfraStructure.Presistence;

public class UserRepository : IUserRepository
{
    private static readonly List<User> _users = new();

    public Task AddUserAsync(User user)
    {
        _users.Add(user);
        return Task.CompletedTask;
    }

    public Task<User?> GetUserByEmailAsync(string email)
    {
        var user = _users.SingleOrDefault(u => u.Email == email);
        return Task.FromResult(user);
    }
}