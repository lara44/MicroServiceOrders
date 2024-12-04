using MediatR;

namespace MicroServiceOrders.Application.Products.Commands;

public class CreatedProductCommand : IRequest
{
        public Guid Id { get; set; }
        public string? Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Stock { get; set; }
}
