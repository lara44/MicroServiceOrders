using MassTransit;
using MediatR;
using MicroServiceOrders.Domain.Product;

namespace MicroServiceOrders.Application.Products.Commands;

public class CreateProductCommandHandler : IRequestHandler<CreatedProductCommand>
{
    private readonly IProductRepository _productRepository;

    public CreateProductCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<Unit> Handle(CreatedProductCommand request, CancellationToken cancellationToken)
    {
        var product = Product.Create(request.Id, request.Name, request.Price, request.Stock);
        await _productRepository.AddAsync(product);
        return Unit.Value;
    }
}
