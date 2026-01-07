using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmigoSecreto.Domain.Entities
{
    public class User
    {
        public int Id { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
        public DateTime CreatedAt { get; private set; }
        public string PasswordHash { get; private set; } = string.Empty;

        public ICollection<Gift> Gifts { get; private set; } = new List<Gift>();

        private User() { }

        public User(string name, string email, string passwordHash)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("User name cannot be null or empty.", nameof(name));
            }
            if (name.Length <= 2)
            {
                throw new ArgumentException("User name must have at least 2 characters.", nameof(name));
            }
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException("Email cannot be null or empty.", nameof(email));
            }
            if (!email.Contains("@"))
            {
                throw new ArgumentException("Email is not valid.", nameof(email));
            }

            if (string.IsNullOrWhiteSpace(passwordHash))
            {
                throw new ArgumentException("Password hash cannot be null or empty.", nameof(passwordHash));
            }

            Name = name;
            Email = email;
            PasswordHash = passwordHash;
            CreatedAt = DateTime.UtcNow;
        }

    }
}