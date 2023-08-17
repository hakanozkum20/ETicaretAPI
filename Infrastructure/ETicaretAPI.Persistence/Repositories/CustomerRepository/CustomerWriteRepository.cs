using ETicaretAPI.Application.Repositories.CustomerRepository;
using ETicaretAPI.Domain.Entities;
using ETicaretAPI.Persistence.Contexts;

namespace ETicaretAPI.Persistence.Repositories.CustomerRepository;

public class CustomerWriteRepository : WriteRepository<Customer>, ICustomerWriteRepository
{
    public CustomerWriteRepository(ETicaretAPIDbContext context) : base(context)
    {
    }
}