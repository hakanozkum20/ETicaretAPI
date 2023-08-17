using ETicaretAPI.Application.Repositories.OrderRepository;
using ETicaretAPI.Domain.Entities;
using ETicaretAPI.Persistence.Contexts;

namespace ETicaretAPI.Persistence.Repositories.OrderRepository;

public class OrderWriteRepository : WriteRepository<Order>, IOrderWriteRepository
{
    public OrderWriteRepository(ETicaretAPIDbContext context) : base(context)
    {
    }
}