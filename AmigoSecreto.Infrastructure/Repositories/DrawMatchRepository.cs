using AmigoSecreto.Domain.Entities;
using AmigoSecreto.Domain.Repositories;
using AmigoSecreto.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AmigoSecreto.Infrastructure.Repositories
{
    public class DrawMatchRepository : IDrawMatchRepository
    {
        private readonly ApplicationDbContext _context;

        public DrawMatchRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(DrawMatch match)
        {
            await _context.DrawMatches.AddAsync(match);
        }

        public async Task AddRangeAsync(IEnumerable<DrawMatch> matches)
        {
            await _context.DrawMatches.AddRangeAsync(matches);
        }

        public async Task<IEnumerable<DrawMatch>> GetByDrawIdAsync(int drawId)
        {
            return await _context.DrawMatches
                .Where(dm => dm.DrawId == drawId)
                .ToListAsync();
        }

        public async Task<DrawMatch?> GetByGiverIdAsync(int giverId, int drawId)
        {
            return await _context.DrawMatches
                .FirstOrDefaultAsync(dm => dm.GiverId == giverId && dm.DrawId == drawId);
        }

        public async Task<DrawMatch?> GetByIdAsync(int id)
        {
            return await _context.DrawMatches.FindAsync(id);
        }

        public Task UpdateAsync(DrawMatch match)
        {
            _context.DrawMatches.Update(match);
            return Task.CompletedTask;
        }
    }
}