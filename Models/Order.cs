namespace Api.Models;

public sealed class Order
{
    public int Id { get; set; }
    public int AccountId { get; set; }
    public string ProductName { get; set; }
    public int Price { get; set; }

    public Account Account { get; set; }
}