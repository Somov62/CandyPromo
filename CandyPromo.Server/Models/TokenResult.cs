namespace CandyPromo.Server.Models;

public class TokenResult
{
    public required string Token { get; set; }
    public bool IsAdmin { get; set; }
    public DateTime Expires { get; set; } = DateTime.Now.AddHours(12);
}