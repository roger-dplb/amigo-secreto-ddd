using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace AmigoSecreto.Infrastructure.Data
{
    /// <summary>
    /// Factory usada pelo EF Core em DESIGN TIME (ao criar migrations).
    /// Permite que 'dotnet ef migrations add' funcione sem ter a aplicação rodando.
    /// </summary>
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            // Configura as opções do DbContext
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlite("Data Source=AmigoSecreto.db");

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}
