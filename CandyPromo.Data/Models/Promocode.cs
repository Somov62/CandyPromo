namespace CandyPromo.Data.Models;
public class Promocode
{
    public required string Code { get; set; }
    public User? Owner { get; set; }
    public Prize? Prize { get; set; }
}