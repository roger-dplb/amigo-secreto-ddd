using AmigoSecreto.Domain.Entities;

namespace AmigoSecreto.Domain.Repositories
{
    public interface IDrawMatchRepository
    {
        Task<DrawMatch?> GetByIdAsync(int id);
        Task<DrawMatch?> GetByGiverIdAsync(int giverId, int drawId);
        Task<IEnumerable<DrawMatch>> GetByDrawIdAsync(int drawId);
        Task AddAsync(DrawMatch match);
        Task AddRangeAsync(IEnumerable<DrawMatch> matches);
        Task UpdateAsync(DrawMatch match);


    }
}