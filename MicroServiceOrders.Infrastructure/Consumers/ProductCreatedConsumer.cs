using System.Data.Common;
using System.Reflection.Metadata.Ecma335;
using MassTransit;
using MediatR;
using MicroServiceOrders.Application.Products.Commands;
using MicroServiceOrders.Domain.Product.Events;
using Microsoft.Extensions.Logging;

namespace MicroServiceOrders.Infrastructure.Consumers;

public class ProductCreatedConsumer : IConsumer<ProductCreatedEvent>
{
    private readonly IMediator _mediator;
    private readonly ILogger<ProductCreatedConsumer> _logger;

    public ProductCreatedConsumer(ILogger<ProductCreatedConsumer> logger, IMediator mediator)
    {
        _mediator = mediator;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<ProductCreatedEvent> context)
    {
        try
        {
            var product = context.Message;
            _logger.LogInformation($"Producto recibido: {product.Name}, Precio: {product.Price}, Stock: {product.Stock}");

            // Enviar el comando al mediador para su procesamiento
            var command = new CreatedProductCommand
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Stock = product.Stock
            };
            await _mediator.Send(command);
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex, "Error procesando el evento");
            throw;
        }
    }
}


