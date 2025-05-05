using Api.Models;

namespace Api.Dtos;

public record AccountDetailsDto : AccountDto
{
    public required List<OrderDto> Orders { get; init; }
    public required Dictionary<string, string> AdditionalDetails { get; init; }

    public static AccountDetailsDto Create(Account account)
    {
        var baseFields = AccountDto.Create(account);

        return new AccountDetailsDto
        {
            Id = baseFields.Id,
            AccountType = baseFields.AccountType,
            Orders = account.Orders.Select(x => new OrderDto
            {
                Id = x.Id,
                AccountId = x.AccountId,
                ProductName = x.ProductName,
                Price = x.Price
            }).ToList(),
            AdditionalDetails = account.GetAdditionalDetails(),
        };
    }
}