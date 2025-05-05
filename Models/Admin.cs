namespace Api.Models;

public class Admin : Account
{
    public required string FirstName { get; init; }
    public required string LastName { get; init; }

    public override bool CanAddOrder(Order order) => true;

    public override Dictionary<string, string> GetAdditionalDetails()
    {
        return new Dictionary<string, string>
        {
            { "FirstName", FirstName },
            { "LastName", LastName }
        };
    }
}