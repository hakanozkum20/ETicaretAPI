using ETicaretAPI.Application.Repositories.CustomerRepository;
using ETicaretAPI.Domain.Entities;
using ETicaretAPI.Persistence.Contexts;

namespace ETicaretAPI.Persistence.Repositories.CustomerRepository;

public class CustomerReadRepository : ReadRepository<Customer> , ICustomerReadRepository
{
    public CustomerReadRepository(ETicaretAPIDbContext context) : base(context)
    {
        
    }
}