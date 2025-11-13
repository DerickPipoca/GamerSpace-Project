using GamerSpace.Domain.Bases;

namespace GamerSpace.Domain.Entities
{
    public class ClassificationType : EntityBase
    {
        public string Name { get; private set; } = null!;

        public ICollection<Category> Categories { get; private set; } = new List<Category>();

        public ClassificationType() { }

        public ClassificationType(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name is required.");
            Name = name;
        }

        public void Update(string? name) => Name = name ?? Name;
    }
}