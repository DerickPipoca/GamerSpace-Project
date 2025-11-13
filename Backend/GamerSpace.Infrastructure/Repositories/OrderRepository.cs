using GamerSpace.Domain.Entities;
using GamerSpace.Domain.Interfaces;
using GamerSpace.Infrastructure.Persistence;

namespace GamerSpace.Infrastructure.Repositories
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(GamerSpaceDbContext gamerSpaceDbContext) : base(gamerSpaceDbContext)
        {
        }
    }
}