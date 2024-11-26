using Infrastructure.Data;
using MassTransit;
using MediatR;
using MicroServiceOrders;
using MicroServiceOrders.Application.Products.Commands;
using MicroServiceOrders.Domain.Product.Events;
using MicroServiceOrders.Infrastructure.Consumers;
using MicroServiceOrders.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuración de DbContext con PostgreSQL
builder.Services.AddDbContext<DataContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IProductRepository, ProductRepository>();

// Registro de MediatR
builder.Services.AddMediatR(typeof(CreateProductCommandHandler).Assembly);

builder.Services.AddMassTransit(configure =>
{
    configure.AddConsumer<ProductCreatedConsumer>();

    configure.UsingAmazonSqs((context, cfg) =>
    {
        cfg.Host(builder.Configuration["AWS:Region"], h =>
        {
            h.AccessKey(builder.Configuration["AWS:AccessKey"]);
            h.SecretKey(builder.Configuration["AWS:SecretKey"]);
        });

        // Configura el endpoint de la cola existente
        cfg.ReceiveEndpoint("dev-product-created-queue", e =>
        {
            e.ConfigureConsumeTopology = false; // Evita la configuración automática de la topología
            e.PrefetchCount = 10; // Número de mensajes que el consumidor puede previsualizar (opcional)
            e.ConfigureConsumer<ProductCreatedConsumer>(context); // Vincula el consumidor
        });

        // Configura el nombre del tema asociado con el evento
        cfg.Message<ProductCreatedEvent>(x =>
        {
            x.SetEntityName("dev_Domain_Product_Events-ProductCreatedEvent"); // Nombre exacto del tema en SNS
        });
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();