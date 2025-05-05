using Api.Dtos;
using Api.Models;
using Api.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("accounts")]
public class AccountsController : ControllerBase
{
    [HttpGet("all")]
    public IActionResult GetAll()
    {
        var accounts = AccountRepository.Accounts.Select(AccountDto.Create).ToList();
        return Ok(accounts);
    }

    [HttpGet("by-account-type/{accountType}")]
    public IActionResult GetByType([FromRoute] string accountType)
    {
        if (!Enum.TryParse<AccountType>(accountType, true, out var type))
        {
            var accountTypes = Enum.GetNames<AccountType>().Select(x => x.ToLower());
            return BadRequest($"Invalid account type: {accountType}. Possible {nameof(AccountType)} values are: ({string.Join(", ", accountTypes)})");
        }

        var accounts = AccountRepository.Accounts
            .Where(x => x.AccountType == type)
            .Select(AccountDto.Create)
            .ToList();

        return Ok(accounts);
    }

    [HttpGet("{id}")]
    public IActionResult GetById([FromRoute] int id)
    {
        var account = AccountRepository.Accounts.SingleOrDefault(x => x.Id == id);
        if (account == null)
        {
            return NotFound($"Account with id {id} was not found");
        }

        var details = AccountDetailsDto.Create(account);
        return Ok(details);
    }

    [HttpPatch("{id}/price-limit")]
    public IActionResult UpdatePriceLimit([FromRoute] int id, [FromBody] UpdatePriceRequest request)
    {
        var account = AccountRepository.Accounts.SingleOrDefault(x => x.Id == id);
        if (account == null)
        {
            return NotFound($"Account with id {id} was not found");
        }

        if (account.AccountType != AccountType.Customer)
        {
            return BadRequest("Cannot update a price for a non customer account");
        }

        (account as Customer)!.PriceLimit = request.PriceLimit;
        return NoContent();
    }
}
