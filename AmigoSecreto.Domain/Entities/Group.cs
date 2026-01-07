namespace AmigoSecreto.Domain.Entities
{
    public class Group
    {
        public int Id { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public string? Description { get; private set; }

        public decimal MinValue { get; private set; }
        public decimal MaxValue { get; private set; }

        public DateTime CreatedAt { get; private set; }
        public int OwnerId { get; private set; }
        public User Owner { get; private set; } = null!;
        public ICollection<User> Moderators { get; private set; } = new List<User>();
        public ICollection<User> Participants { get; private set; } = new List<User>();
        public Draw? Draw { get; private set; }

        public DateTime HappenAt { get; private set; }

        private Group() { }

        public Group(string name, string description, decimal minValue, decimal maxValue, int ownerId, DateTime happenAt)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Group name cannot be null or empty.", nameof(name));
            }
            if (name.Length < 3)
            {
                throw new ArgumentException("Group name must have at least 3 characters.", nameof(name));
            }
            if (description != null && description.Length > 500)

            {
                throw new ArgumentException("Description cannot exceed 500 characters.", nameof(description));

            }
            if (minValue < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(MinValue), "Minimum value cannot be negative.");
            }
            if (maxValue <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(MaxValue), "Maximum value must be greater than zero.");
            }
            if (minValue >= maxValue)
            {
                throw new ArgumentException("Minimum value must be less than maximum value.");
            }

            if (happenAt == default)
            {
                throw new ArgumentException("HappenAt must be a valid date.", nameof(happenAt));
            }


            Name = name;
            Description = description;
            CreatedAt = DateTime.UtcNow;
            MinValue = minValue;
            MaxValue = maxValue;
            OwnerId = ownerId;
            HappenAt = happenAt;

        }
        public bool IsUserModerator(int userId)
        {
            return Moderators.Any(m => m.Id == userId);
        }

        public bool IsUserMember(int userId)
        {
            return Participants.Any(m => m.Id == userId);
        }

    }
}