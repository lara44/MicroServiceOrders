using MicroServiceOrders.Domain.Product;

namespace MicroServiceOrders;

public interface IProductRepository
{
    Task AddAsync(Product product);
}
