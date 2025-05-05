namespace Api.Dtos;

public record OrderDto
{
    public required int Id { get; init; }
    public required int AccountId { get; init; }
    public required string ProductName { get; set; }
    public required int Price { get; init; }
}