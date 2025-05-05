namespace Api.Controllers;

public record UpdatePriceRequest
{
    public required int PriceLimit { get; init; }
}