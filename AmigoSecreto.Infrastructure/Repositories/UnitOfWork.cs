using AmigoSecreto.Domain.Repositories;

namespace AmigoSecreto.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public IUserRepository Users => throw new NotImplementedException();

        public IGiftRepository Gifts => throw new NotImplementedException();

        public IGroupRepository Groups => throw new NotImplementedException();

        public IDrawRepository Draws => throw new NotImplementedException();

        public IDrawMatchRepository DrawMatches => throw new NotImplementedException();

        public Task BeginTransactionAsync()
        {
            throw new NotImplementedException();
        }

        public Task CommitTransactionAsync()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task RollbackTransactionAsync()
        {
            throw new NotImplementedException();
        }

        public Task<int> SaveChangesAsync()
        {
            throw new NotImplementedException();
        }
    }
}