using MediatR;

namespace ETicaretAPI.Application.Features.Queries.ProductImageFile.GetProductImageFiles;

public class GetProductImageFilesQueryRequest : IRequest<List<GetProductImageFilesQueryResponse>>
{
    public string Id { get; set; }
}