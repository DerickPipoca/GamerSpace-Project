using GamerSpace.Domain.Entities;

namespace GamerSpace.Domain.Interfaces
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<IEnumerable<Category>> GetAllWithDetailsAsync();
        Task<Category?> GetByIdWithDetailsAsync(long categoryId);
    }
}