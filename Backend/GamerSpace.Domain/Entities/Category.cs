using GamerSpace.Domain.Bases;

namespace GamerSpace.Domain.Entities
{
    public class Category : EntityBase
    {
        public string Name { get; private set; } = string.Empty;

        public long TypeId { get; private set; }
        public ClassificationType Type { get; private set; } = null!;

        public ICollection<ProductCategory> ProductCategories { get; private set; } = new List<ProductCategory>();

        private Category() { }

        public Category(string name, long typeId)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name is required.");
            if(typeId <= 0)
                throw new ArgumentException("Type id is invalid.");
            Name = name;
            TypeId = typeId;
        }

        public void Update(string? name) => Name = name ?? Name;
    }
}