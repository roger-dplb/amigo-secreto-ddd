using AmigoSecreto.Domain.Enums;

namespace AmigoSecreto.Domain.Entities
{
    public class Draw
    {
        // Propriedades
        public int Id { get; private set; }
        public int GroupId { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? CompletedAt { get; private set; }
        public DrawStatus Status { get; private set; }

        // Navigation Properties
        public Group Group { get; private set; } = null!;
        public ICollection<DrawMatch> DrawMatches { get; private set; } = new List<DrawMatch>();

        // Construtor privado para EF Core
        private Draw() { }

        // Construtor público
        public Draw(int groupId)
        {
            if (groupId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(groupId), "Group ID must be greater than zero.");
            }

            GroupId = groupId;
            CreatedAt = DateTime.UtcNow;
            Status = DrawStatus.Pending;
        }

        // Métodos de domínio
        public void Complete()
        {
            if (Status == DrawStatus.Completed)
            {
                throw new InvalidOperationException("Draw is already completed.");
            }

            if (Status == DrawStatus.Cancelled)
            {
                throw new InvalidOperationException("Cannot complete a cancelled draw.");
            }

            if (!DrawMatches.Any())
            {
                throw new InvalidOperationException("Cannot complete draw without matches.");
            }

            Status = DrawStatus.Completed;
            CompletedAt = DateTime.UtcNow;
        }

        public void Cancel()
        {
            if (Status == DrawStatus.Completed)
            {
                throw new InvalidOperationException("Cannot cancel a completed draw.");
            }

            Status = DrawStatus.Cancelled;
        }

        public bool CanUserSeeResult(int userId)
        {
            return Status == DrawStatus.Completed &&
                   DrawMatches.Any(m => m.GiverId == userId);
        }

        public DrawMatch? GetMatchForUser(int userId)
        {
            if (Status != DrawStatus.Completed)
                return null;

            return DrawMatches.FirstOrDefault(m => m.GiverId == userId);
        }
    }
}