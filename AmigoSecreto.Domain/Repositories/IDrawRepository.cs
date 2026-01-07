using AmigoSecreto.Domain.Entities;
namespace AmigoSecreto.Domain.Repositories
{
    public interface IDrawRepository
    {
        Task<Draw?> GetByIdAsync(int id);
        Task<Draw?> GetByGroupIdAsync(int groupId);  // Um grupo só tem 1 draw
        Task AddAsync(Draw draw);
        Task UpdateAsync(Draw draw);  // Para marcar como Complete/Cancel
        Task RemoveAsync(int drawId);

    }
}