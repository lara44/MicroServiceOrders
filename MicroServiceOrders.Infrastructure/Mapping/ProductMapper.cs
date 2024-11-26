namespace Infrastructure.Mapping;

public class ProductMapper
{
     public static Infrastructure.Data.Entities.Product ToProductEntity(MicroServiceOrders.Domain.Product.Product product)
        {

            return new Infrastructure.Data.Entities.Product
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Stock = product.Stock,
            };
        }
        public static MicroServiceOrders.Domain.Product.Product ToDomainProduct(Infrastructure.Data.Entities.Product product)
        {
            return MicroServiceOrders.Domain.Product.Product.Create(
                product.Id,
                product.Name,
                product.Price,
                product.Stock
            );
        }
}
