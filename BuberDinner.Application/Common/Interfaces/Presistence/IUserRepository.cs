using BuberDinner.Domain.Entities;

namespace BuberDinner.Application.Common.Interfaces.Presistence;
public interface IUserRepository
{
    Task AddUserAsync(User user);
    Task<User?> GetUserByEmailAsync(string email);
}