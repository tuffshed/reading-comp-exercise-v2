using Api.Models;
using Api.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

public record CreateOrderRequest
{
    public required int AccountId { get; init; }
    public required string ProductName { get; init; }
    public required int Price { get; init; }
}

[ApiController]
[Route("orders")]
public class OrdersController : ControllerBase
{
    [HttpPut]
    public IActionResult Create([FromBody] CreateOrderRequest request)
    {
        var account = AccountRepository.Accounts.SingleOrDefault(x => x.Id == request.AccountId);

        if (account == null)
        {
            return NotFound($"Account with id {request.AccountId} was not found");
        }

        var allOrders = AccountRepository.Accounts.SelectMany(x => x.Orders).ToList();

        var order = new Order
        {
            Id = !allOrders.Any() ? 1 : allOrders.Max(x => x.Id) + 1,
            AccountId = request.AccountId,
            Account = account,
            ProductName = request.ProductName,
            Price = request.Price
        };

        if (!account.CanAddOrder(order))
        {
            return UnprocessableEntity($"Account with id {request.AccountId} cannot add order with price {request.Price}.");
        }

        account.Orders.Add(order);
        return CreatedAtAction(nameof(Create), request, new { id = order.Id });
    }
}