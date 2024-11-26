using MassTransit;

namespace MicroServiceOrders.Domain.Product.Events;

[MessageUrn("ProductCreatedEvent")]
public class ProductCreatedEvent
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
}
