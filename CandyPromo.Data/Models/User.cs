namespace CandyPromo.Data.Models;

public class User
{
    public required Guid Id { get; set; } = Guid.NewGuid();
    public required string Name { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public required string Password { get; set; }
    public bool IsAdmin { get; set; }

    public List<Promocode>? Promocodes { get; set; }
}