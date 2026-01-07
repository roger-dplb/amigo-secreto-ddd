

using Microsoft.EntityFrameworkCore;
using AmigoSecreto.Domain.Entities;
namespace AmigoSecreto.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<User> Users => Set<User>();
        public DbSet<Gift> Gifts => Set<Gift>();
        public DbSet<Group> Groups => Set<Group>();
        public DbSet<Draw> Draws => Set<Draw>();
        public DbSet<DrawMatch> DrawMatches => Set<DrawMatch>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ====================================
            // 🔧 Configuração da entidade User
            // ====================================
            modelBuilder.Entity<User>(entity =>
            {
                // 1️⃣ Configura a chave primária
                entity.HasKey(u => u.Id);

                // 2️⃣ Configura propriedades de string (tamanho máximo)
                entity.Property(u => u.Name)
                    .IsRequired()           // NOT NULL no banco
                    .HasMaxLength(100);     // VARCHAR(100)

                entity.Property(u => u.Email)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(u => u.PasswordHash)
                    .IsRequired()
                    .HasMaxLength(500);     // Hash pode ser grande

                // 3️⃣ Cria índice único no Email (não pode repetir)
                entity.HasIndex(u => u.Email)
                    .IsUnique();

                // 4️⃣ Configura relacionamento User -> Gifts (um-para-muitos)
                entity.HasMany(u => u.Gifts)
                    .WithOne()              // Gift não tem navigation property de volta para User
                    .HasForeignKey(g => g.UserId)
                    .OnDelete(DeleteBehavior.Cascade);  // Se deletar User, deleta os Gifts
            });

            //Gift
            modelBuilder.Entity<Gift>(entity =>
            {
                entity.HasKey(g => g.Id);
                entity.Property(g => g.Name)
                    .IsRequired()
                    .HasMaxLength(200);
                entity.Property(g => g.Description)
                    .HasMaxLength(500);
                entity.Property(g => g.EstimatedValue)
                    .IsRequired()
                    .HasPrecision(18, 2);
            });

            //Group
            modelBuilder.Entity<Group>(entity =>
            {
                entity.HasKey(g => g.Id);
                entity.Property(g => g.Name)
                    .IsRequired()
                    .HasMaxLength(200);
                entity.Property(g => g.Description)
                    .HasMaxLength(500);
                entity.Property(g => g.MinValue)
                    .IsRequired()
                    .HasPrecision(18, 2);
                entity.Property(g => g.MaxValue)
                    .IsRequired()
                    .HasPrecision(18, 2);
                entity.HasMany(g => g.Moderators)
                    .WithMany()
                    .UsingEntity(j => j.ToTable("GroupModerators"));
                entity.HasMany(g => g.Participants)
                    .WithMany()
                    .UsingEntity(j => j.ToTable("GroupParticipants"));
                entity.HasOne(g => g.Owner)
                    .WithMany()
                    .HasForeignKey(g => g.OwnerId)
                    .OnDelete(DeleteBehavior.Restrict);
                entity.Property(g => g.HappenAt)
                    .IsRequired();

                entity.Property(g => g.CreatedAt)
                    .IsRequired();
                entity.HasOne(g => g.Draw)
                    .WithOne(d => d.Group)
                    .HasForeignKey<Draw>(d => d.GroupId)
                    .OnDelete(DeleteBehavior.Cascade);

            });
            //Draw
            modelBuilder.Entity<Draw>(entity =>
            {
                entity.HasKey(d => d.Id);
                entity.Property(d => d.CreatedAt);
                entity.Property(d => d.Status);
                entity.HasMany(d => d.DrawMatches)
                    .WithOne(dm => dm.Draw)
                    .HasForeignKey(dm => dm.DrawId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<DrawMatch>(entity =>
            {
                entity.HasKey(dm => dm.Id);

                // Relacionamento DrawMatch -> User (Giver)
                entity.HasOne(dm => dm.Giver)
                    .WithMany()  // User não tem lista de DrawMatches como Giver
                    .HasForeignKey(dm => dm.GiverId)
                    .OnDelete(DeleteBehavior.Restrict);

                // Relacionamento DrawMatch -> User (Receiver)
                entity.HasOne(dm => dm.Receiver)
                    .WithMany()  // User não tem lista de DrawMatches como Receiver
                    .HasForeignKey(dm => dm.ReceiverId)
                    .OnDelete(DeleteBehavior.Restrict);  // ⚠️ Não pode deletar User se ele for Receiver
            });

        }

    }
}
