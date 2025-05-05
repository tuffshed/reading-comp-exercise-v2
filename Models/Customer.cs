namespace Api.Models;

public class Customer : Account
{
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public required int PriceLimit { get; set; }

    public override bool CanAddOrder(Order order) => order.Price <= PriceLimit;

    public override Dictionary<string, string> GetAdditionalDetails()
    {
        return new Dictionary<string, string>
        {
            { "FirstName", FirstName },
            { "LastName", LastName },
            { "PriceLimit", PriceLimit.ToString() }
        };
    }
}