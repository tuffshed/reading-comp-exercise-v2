namespace Api.Models;

public class AccountGroup : Account
{
    public List<Account> Accounts { get; init; } = [];
    public required string Name { get; set; }

    public override bool CanAddOrder(Order order)
    {
        foreach (var account in Accounts)
        {
            if (account.CanAddOrder(order))
            {
                return true;
            }
        }

        return false;
    }

    public override Dictionary<string, string> GetAdditionalDetails()
    {
        return new Dictionary<string, string>
        {
            { "Name", Name },
            { "AccountCount", Accounts.Count.ToString() }
        };
    }
}