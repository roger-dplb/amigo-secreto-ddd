using AmigoSecreto.Domain.Entities;
using AmigoSecreto.Domain.Repositories;
using AmigoSecreto.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AmigoSecreto.Infrastructure.Repositories
{
    /// <summary>
    /// Implementação do repositório de usuários usando EF Core.
    /// </summary>
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        // ====================================
        // 🔧 Construtor - Recebe o DbContext via Dependency Injection
        // ====================================
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // ====================================
        // 📖 GET BY EMAIL
        // ====================================
        public async Task<User?> GetByEmailAsync(string email)
        {
            // FirstOrDefaultAsync retorna null se não encontrar
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        // ====================================
        // 📖 GET BY ID
        // ====================================
        public async Task<User?> GetByIdAsync(int id)
        {
            // FindAsync é otimizado para buscar por Primary Key
            return await _context.Users.FindAsync(id);
        }

        // ====================================
        // ➕ ADD (Criar)
        // ====================================
        public async Task AddAsync(User user)
        {
            // AddAsync adiciona a entidade ao contexto (em memória)
            // SaveChanges (feito no UnitOfWork) persiste no banco
            await _context.Users.AddAsync(user);
        }

        // ====================================
        // 🔄 UPDATE (Atualizar)
        // ====================================
        public Task UpdateAsync(User user)
        {
            // Update marca a entidade como modificada
            // SaveChanges (feito no UnitOfWork) persiste as mudanças
            _context.Users.Update(user);

            // Retorna Task.CompletedTask porque não há operação assíncrona aqui
            return Task.CompletedTask;
        }

        // ====================================
        // ❌ REMOVE (Deletar)
        // ====================================
        public async Task RemoveAsync(int userId)
        {
            // Primeiro busca o usuário
            var user = await _context.Users.FindAsync(userId);

            if (user == null)
            {
                throw new KeyNotFoundException($"Usuário com ID {userId} não encontrado.");
            }
            _context.Users.Remove(user);
        }
    }
}