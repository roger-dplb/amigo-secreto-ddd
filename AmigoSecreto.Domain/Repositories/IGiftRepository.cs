using AmigoSecreto.Domain.Entities;

namespace AmigoSecreto.Domain.Repositories
{
    public interface IGiftRepository
    {
        public Task<Gift?> GetByIdAsync(int id);
        public Task AddAsync(Gift gift);
        public Task RemoveAsync(int giftId);

        public Task UpdateAsync(Gift gift);

        public Task<IEnumerable<Gift>> GetByUserIdAsync(int userId);
    }
}