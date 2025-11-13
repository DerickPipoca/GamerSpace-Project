using System.Linq.Expressions;
using System.Reflection;
using GamerSpace.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GamerSpace.Infrastructure.Persistence
{
    public class GamerSpaceDbContext : DbContext
    {

        public GamerSpaceDbContext(DbContextOptions<GamerSpaceDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Address> Addresses { get; set; }
        //To-do later
        //public DbSet<CartProduct> CartProducts { get; set; }
        //public DbSet<Payment> Payments { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductVariant> ProductVariants { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ClassificationType> ClassificationTypes { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (entityType.FindProperty("Disabled") is not null)
                {
                    modelBuilder.Entity(entityType.ClrType)
                        .HasQueryFilter(ConvertToDeleteExpression(entityType.ClrType));
                }
            }

            base.OnModelCreating(modelBuilder);
        }

        private static LambdaExpression ConvertToDeleteExpression(Type entityType)
        {
            var parameter = Expression.Parameter(entityType, "e");
            var property = Expression.Property(parameter, "Disabled");
            var negatedProperty = Expression.Not(property);
            return Expression.Lambda(negatedProperty, parameter);
        }
    }
}