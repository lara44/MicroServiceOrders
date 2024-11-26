namespace MicroServiceOrders.Domain.Product;

public class Product
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public decimal Price { get; private set; }
    public int Stock { get; private set; }

    private Product(Guid id, string name, decimal price, int stock)
    {
        Id = id;
        Name = name;
        Price = price;
        Stock = stock;
    }

    public static Product Create(Guid id, string name, decimal price, int stock)
    {
        var product = new Product(id, name, price, stock);
        return product;
    }
}
