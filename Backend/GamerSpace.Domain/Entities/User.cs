using GamerSpace.Domain.Bases;

namespace GamerSpace.Domain.Entities
{
    public class User : EntityBase
    {
        public string FullName { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
        public string PasswordHash { get; private set; } = string.Empty;
        public string Role { get; private set; } = string.Empty;

        public ICollection<Address> Addresses { get; private set; } = new List<Address>();

        public ICollection<Order> Orders { get; private set; } = new List<Order>();

        //public ICollection<CartProduct> CartProducts { get; set; } = new List<CartProduct>();

        private User() { }
        
        public User(string fullName, string email, string passwordHash, string role)
        {
            if (string.IsNullOrWhiteSpace(fullName))
                throw new ArgumentException("Full name is required.");

            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email is required.");

            if (string.IsNullOrWhiteSpace(role))
                throw new ArgumentException("Role is required.");

            if (string.IsNullOrWhiteSpace(passwordHash))
                throw new ArgumentException("Password hash is required.");

            FullName = fullName;
            Email = email;
            PasswordHash = passwordHash;
            Role = role;
        }
    }
}