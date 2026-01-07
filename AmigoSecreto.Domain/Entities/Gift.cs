namespace AmigoSecreto.Domain.Entities
{
    public class Gift
    {
        public int Id { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public string? Description { get; private set; }
        public int UserId { get; private set; }

        public decimal EstimatedValue { get; private set; }
        private Gift() { }

        public Gift(string name, decimal estimatedValue, int userId, string? description = null)
        {
            // Validação 1: Nome não pode ser vazio
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Gift name cannot be null or empty.", nameof(name));
            }

            // Validação 2: Nome mínimo 3 caracteres
            if (name.Length < 3)
            {
                throw new ArgumentException("Gift name must have at least 3 characters.", nameof(name));
            }

            // Validação 3: Valor deve ser maior que zero
            if (estimatedValue <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(estimatedValue), "Estimated value must be greater than zero.");
            }

            // Validação 4: Descrição máximo 500 caracteres (boa adição sua!)
            if (description != null && description.Length > 500)
            {
                throw new ArgumentException("Description cannot exceed 500 characters.", nameof(description));
            }

            // Agora sim atribui os valores
            UserId = userId;
            Name = name;
            EstimatedValue = estimatedValue;
            Description = description;
        }
        public bool FitsInBudget(decimal minValue, decimal maxValue)
        {
            return EstimatedValue >= minValue && EstimatedValue <= maxValue;
        }
    }

}