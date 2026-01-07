using AmigoSecreto.Domain.Entities;

namespace AmigoSecreto.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetByEmailAsync(string email);

        Task<User?> GetByIdAsync(int id);
        Task AddAsync(User user);
        Task RemoveAsync(int userId);

        Task UpdateAsync(User user);

    }
}