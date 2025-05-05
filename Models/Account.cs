namespace Api.Models;

public abstract class Account
{
    public required AccountType AccountType { get; set; }
    public required int Id { get; set; }

    public List<Order> Orders { get; set; } = [];

    public abstract bool CanAddOrder(Order order);
    public abstract Dictionary<string, string> GetAdditionalDetails();
}
