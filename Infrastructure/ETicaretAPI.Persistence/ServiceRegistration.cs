using ETicaretAPI.Application.Repositories.CustomerRepository;
using ETicaretAPI.Application.Repositories.OrderRepository;
using ETicaretAPI.Application.Repositories.ProductRepository;
using Microsoft.EntityFrameworkCore;
using ETicaretAPI.Persistence.Contexts;
using ETicaretAPI.Persistence.Repositories.CustomerRepository;
using ETicaretAPI.Persistence.Repositories.OrderRepository;
using ETicaretAPI.Persistence.Repositories.ProductRepository;
using Microsoft.Extensions.DependencyInjection;
using ETicaretAPI.Application.Repositories.ProductImageFileRepository;
using ETicaretAPI.Persistence.Repositories.ProductImageFileRepository;
using ETicaretAPI.Application.Repositories.FileRepository;
using ETicaretAPI.Persistence.Repositories.FileRepository;
using ETicaretAPI.Application.Repositories.InvoiceFileRepository;
using ETicaretAPI.Persistence.Repositories.InvioceFileRepository;

namespace ETicaretAPI.Persistence;

public static class ServiceRegistration
{
    public static void AddPersistenceServices(this IServiceCollection services)
    {
        services.AddDbContext<ETicaretAPIDbContext>(options =>
            options.UseNpgsql(Configuration.ConnectionString));
        
        services.AddScoped<ICustomerReadRepository, CustomerReadRepository>();
        services.AddScoped<ICustomerWriteRepository, CustomerWriteRepository>();
        services.AddScoped<IOrderReadRepository, OrderReadRepository>();
        services.AddScoped<IOrderWriteRepository, OrderWriteRepository>();
        services.AddScoped<IProductReadRepository, ProductReadRepository>();
        services.AddScoped<IProductWriteRepository, ProductWriteRepository>();
        services.AddScoped<IProductImageFileReadRepository, ProductImageFileReadRepository>();
        services.AddScoped<IProductImageFileWriteRepository, ProductImageFileWriteRepository>();
        services.AddScoped<IFileWriteRepository, FileWriteRepository>();
        services.AddScoped<IFileReadRepository, FileReadRepository>();
        services.AddScoped<IInvoiceFileWriteRepository, InvoiceFileWriteRepository>();
        services.AddScoped<IInvoiceFileReadRepository, InvoiceFileReadRepository>();
    }
}