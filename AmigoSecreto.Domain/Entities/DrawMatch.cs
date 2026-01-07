namespace AmigoSecreto.Domain.Entities
{
    public class DrawMatch
    {
        public int Id { get; private set; }
        public int DrawId { get; private set; }
        public Draw Draw { get; private set; } = null!;
        public int GiverId { get; private set; }
        public User Giver { get; private set; } = null!;
        public int ReceiverId { get; private set; }
        public User Receiver { get; private set; } = null!;
        public DateTime? RevealedAt { get; private set; }

        public DateTime CreatedAt { get; private set; }

        private DrawMatch() { }

        public DrawMatch(int drawId, int giverId, int receiverId)
        {
            if (drawId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(drawId), "DrawId must be greater than zero.");
            }
            if (giverId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(giverId), "GiverId must be greater than zero.");
            }
            if (receiverId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(receiverId), "ReceiverId must be greater than zero.");
            }
            if (giverId == receiverId)
            {
                throw new ArgumentException("GiverId and ReceiverId cannot be the same.");
            }
            DrawId = drawId;
            GiverId = giverId;
            ReceiverId = receiverId;
            CreatedAt = DateTime.UtcNow;

        }
        public void MarkAsRevealed()
        {
            if (RevealedAt.HasValue)
            {
                throw new InvalidOperationException("Draw match is already revealed.");
            }
            RevealedAt = DateTime.UtcNow;
        }

    }
}