using GamerSpace.Domain.Bases;

namespace GamerSpace.Domain.Entities
{
    public class Product : EntityBase
    {
        public string Name { get; private set; } = string.Empty;
        public string? Description { get; private set; }

        public ICollection<ProductCategory> ProductCategories { get; private set; } = new List<ProductCategory>();

        public ICollection<ProductVariant> Variants { get; private set; } = new List<ProductVariant>();

        private Product() { }

        public Product(string name, string? description)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name is required.");
            Name = name;
            Description = description;
        }

        public void Update(string? name, string? description, List<long>? categories)
        {
            Name = name ?? Name;
            Description = description ?? Description;
            if (categories != null)
            {
                var categoriesToRemove = ProductCategories
                    .Where(pc => !categories.Contains(pc.CategoryId))
                    .ToList();

                foreach (var categoryToRemove in categoriesToRemove)
                {
                    ProductCategories.Remove(categoryToRemove);
                }

                var existingCategoryIds = ProductCategories.Select(pc => pc.CategoryId).ToList();

                var categoriesToAdd = categories
                    .Where(categoryId => !existingCategoryIds.Contains(categoryId))
                    .ToList();

                foreach (var categoryId in categoriesToAdd)
                {
                    ProductCategories.Add(new ProductCategory
                    {
                        ProductId = this.Id,
                        CategoryId = categoryId
                    });
                }
            }
        }

        public void AddVariant(ProductVariant variant)
        {
            Variants.Add(variant);
        }

        public void UpdateVariant(long productVariantId, string? sku, string? description, decimal? price, short? stockAmount, string? imageUrl)
        {
            var productVariant = Variants.FirstOrDefault(x => x.Id == productVariantId);
            if (productVariant == null)
                throw new KeyNotFoundException($"ProductVariant with ID {productVariantId} not found.");

            productVariant.Update(sku, price, description, stockAmount, imageUrl);
        }

        public void RemoveVariant(long productVariantId)
        {
            if (Variants.Count(pv => !pv.Disabled) <= 1)
                throw new InvalidOperationException("A product must have at least one active variant.");

            var variantToRemove = this.Variants.FirstOrDefault(v => v.Id == productVariantId);

            if (variantToRemove == null)
                throw new KeyNotFoundException($"Variant with ID {productVariantId} not found in this product.");

            variantToRemove.Disable();
        }

        public override void Disable()
        {
            base.Disable();

            foreach (var variant in Variants)
            {
                variant.Disable();
            }
        }
    }
}