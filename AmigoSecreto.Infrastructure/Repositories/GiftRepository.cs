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
    public class GiftRepository : IGiftRepository
    {
        private readonly ApplicationDbContext _context;

        public GiftRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Gift gift)
        {

            await _context.Gifts.AddAsync(gift);
        }

        public async Task<Gift?> GetByIdAsync(int id)
        {
            return await _context.Gifts.FindAsync(id);
        }

        public async Task<IEnumerable<Gift>> GetByUserIdAsync(int userId)
        {
            return await _context.Gifts
                .Where(g => g.UserId == userId)
                .ToListAsync();
        }

        public async Task RemoveAsync(int giftId)
        {
            var gift = await _context.Gifts.FindAsync(giftId);
            if (gift != null)
            {
                _context.Gifts.Remove(gift);
            }

        }

        public async Task UpdateAsync(Gift gift)
        {
            var existingGift = await _context.Gifts.FindAsync(gift.Id);
            if (existingGift == null)
            {
                throw new KeyNotFoundException($"Presente com ID {gift.Id} não encontrado.");
            }
            _context.Gifts.Update(gift);

        }
    }
}