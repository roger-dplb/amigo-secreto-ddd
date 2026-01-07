namespace AmigoSecreto.Domain.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        IGiftRepository Gifts { get; }
        IGroupRepository Groups { get; }
        IDrawRepository Draws { get; }
        IDrawMatchRepository DrawMatches { get; }

        Task<int> SaveChangesAsync();
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
    }
}