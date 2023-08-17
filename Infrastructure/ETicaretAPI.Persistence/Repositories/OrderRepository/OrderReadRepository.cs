using ETicaretAPI.Application.Repositories.OrderRepository;
using ETicaretAPI.Domain.Entities;
using ETicaretAPI.Persistence.Contexts;

namespace ETicaretAPI.Persistence.Repositories.OrderRepository;

public class OrderReadRepository : ReadRepository<Order>, IOrderReadRepository
{
    public OrderReadRepository(ETicaretAPIDbContext context) : base(context)
    {
    }
}