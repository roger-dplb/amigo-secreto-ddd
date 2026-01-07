using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AmigoSecreto.Domain.Entities;
using AmigoSecreto.Domain.Repositories;
using AmigoSecreto.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
namespace AmigoSecreto.Infrastructure.Repositories
{
    public class DrawRepository : IDrawRepository
    {
        private readonly ApplicationDbContext _context;
        public DrawRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Draw draw)
        {
            await _context.Draws.AddAsync(draw);
        }

        public async Task<Draw?> GetByGroupIdAsync(int groupId)
        {
            return await _context.Draws.FirstOrDefaultAsync(d => d.GroupId == groupId);
        }

        public async Task<Draw?> GetByIdAsync(int id)
        {
            return await _context.Draws.FindAsync(id);
        }

        public async Task RemoveAsync(int drawId)
        {
            var draw = await _context.Draws.FindAsync(drawId);
            if (draw != null)
            {
                _context.Draws.Remove(draw);
            }

        }

        public async Task UpdateAsync(Draw draw)
        {
            var existingDraw = await _context.Draws.FindAsync(draw.Id);
            if (existingDraw == null)
            {
                throw new KeyNotFoundException($"Sorteio com ID {draw.Id} não encontrado.");
            }

            _context.Draws.Update(draw);
        }
    }
}