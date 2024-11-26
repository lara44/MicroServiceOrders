namespace MicroServiceOrders;

public class Order
{
    private Guid Id { get; init; }
    private DateTime DateOrder { get; init; }
    private Decimal TotalOrder { get; init; }

    private Order(Guid id, DateTime dateOrder, Decimal totalOrder)
    {
        Id = id;
        DateOrder = dateOrder;
        TotalOrder = totalOrder;
    }

    public static Order Create(Guid id, DateTime dateOrder, Decimal totalOrder)
    {
        var order = new Order(id, dateOrder, totalOrder);
        return order;
    }
}
