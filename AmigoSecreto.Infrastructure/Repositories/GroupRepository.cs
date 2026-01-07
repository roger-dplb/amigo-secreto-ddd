using AmigoSecreto.Domain.Entities;
using AmigoSecreto.Domain.Repositories;
using AmigoSecreto.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AmigoSecreto.Infrastructure.Repositories
{
    public class GroupRepository : IGroupRepository
    {
        private readonly ApplicationDbContext _context;

        public GroupRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Group group)
        {
            await _context.Groups.AddAsync(group);
        }

        public async Task<bool> AddModeratorAsync(int groupId, int userId)
        {
            // 1. Busca o grupo (com a coleção de moderadores carregada)
            var group = await _context.Groups
                .Include(g => g.Moderators)  // Carrega a coleção de moderadores
                .FirstOrDefaultAsync(g => g.Id == groupId);

            if (group == null)
            {
                return false;  // Grupo não encontrado
            }

            // 2. Busca o usuário
            var user = await _context.Users.FindAsync(userId);

            if (user == null)
            {
                return false;  // Usuário não encontrado
            }

            // 3. Verifica se já não é moderador
            if (group.Moderators.Any(m => m.Id == userId))
            {
                return false;  // Já é moderador
            }

            // 4. Adiciona o usuário à coleção de moderadores
            group.Moderators.Add(user);

            // 5. EF Core automaticamente cria o registro na tabela pivot
            // quando você chamar SaveChangesAsync no UnitOfWork

            return true;
        }

        public async Task<bool> AddParticipantAsync(int groupId, int userId)
        {
            var group = await _context.Groups
                .Include(g => g.Participants)
                .FirstOrDefaultAsync(g => g.Id == groupId);
            if (group == null)
            {
                return false;
            }
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                return false;

            }
            if (group.Participants.Any(p => p.Id == userId))
            {
                return false;
            }
            group.Participants.Add(user);
            return true;
        }

        public async Task<Group?> GetByIdAsync(int id)
        {
            return await _context.Groups.FindAsync(id);
        }

        public async Task<IEnumerable<Group>> GetByUserIdAsync(int userId)
        {
            return await _context.Groups
                 .Where(g => g.Participants.Any(p => p.Id == userId) || g.Moderators.Any(m => m.Id == userId))
                 .ToListAsync();
        }

        public async Task<IEnumerable<User>> GetModeratorsAsync(int groupId)
        {
            return await _context.Groups
                .Where(g => g.Id == groupId)
                .SelectMany(g => g.Moderators)
                .ToListAsync();
        }


        public async Task<IEnumerable<User>> GetParticipantsAsync(int groupId)
        {
            return await _context.Groups
                .Where(g => g.Id == groupId)
                .SelectMany(g => g.Participants)
                .ToListAsync();
        }

        public async Task RemoveAsync(int groupId)
        {
            var group = await _context.Groups.FindAsync(groupId);
            if (group != null)
            {
                _context.Groups.Remove(group);
            }

        }

        public Task<bool> RemoveModeratorAsync(int groupId, int userId)
        {
            var group = _context.Groups
                .Include(g => g.Moderators)
                .FirstOrDefault(g => g.Id == groupId);
            if (group == null)
            {
                return Task.FromResult(false);
            }
            var user = group.Moderators.FirstOrDefault(m => m.Id == userId);
            if (user == null)
            {
                return Task.FromResult(false);

            }
            group.Moderators.Remove(user);
            return Task.FromResult(true);
        }

        public Task<bool> RemoveParticipantAsync(int groupId, int userId)
        {
            var group = _context.Groups
                .Include(g => g.Participants)
                .FirstOrDefault(g => g.Id == groupId);
            if (group == null)
            {
                return Task.FromResult(false);
            }
            var user = group.Participants.FirstOrDefault(m => m.Id == userId);
            if (user == null)
            {
                return Task.FromResult(false);

            }
            group.Participants.Remove(user);
            return Task.FromResult(true);
        }

        public Task UpdateAsync(Group group)
        {
            _context.Groups.Update(group);
            return Task.CompletedTask;
        }
    }
}