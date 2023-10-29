using ETicaretAPI.Application.Repositories.ProductRepository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ETicaretAPI.Application.Features.Queries.ProductImageFile.GetProductImageFiles;

public class GetProductImageFilesQueryHandler : IRequestHandler<GetProductImageFilesQueryRequest, List<GetProductImageFilesQueryResponse>>
{

    private IProductReadRepository _productReadRepository;
    private IConfiguration configuration;


    public GetProductImageFilesQueryHandler(IProductReadRepository productReadRepository, IConfiguration configuration)
    {
        _productReadRepository = productReadRepository;
        this.configuration = configuration;
    }

    public async Task<List<GetProductImageFilesQueryResponse>> Handle(GetProductImageFilesQueryRequest request, CancellationToken cancellationToken)
    {
        Domain.Entities.Product? product = await _productReadRepository.Table.Include(p=> p.ProductImageFiles).FirstOrDefaultAsync(p=> p.Id==Guid.Parse(request.Id));
        return product?.ProductImageFiles.Select(p => new GetProductImageFilesQueryResponse()
        {
            Path = $"{configuration["BaseStorageUrl"]}/{p.Path}",
            FileName = p.FileName,
            Id = p.Id
        }).ToList();

    }
}