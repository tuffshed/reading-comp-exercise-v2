using Api.Models;

namespace Api.Dtos;

public record AccountDto
{
    public required int Id { get; init; }
    public required string AccountType { get; init; }

    public static AccountDto Create(Account account)
    {
        return new AccountDto
        {
            Id = account.Id,
            AccountType = account.AccountType.ToString(),
        };
    }
}

