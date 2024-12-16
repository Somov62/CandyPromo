namespace CandyPromo.Data.Models;

/// <summary>
/// Промокод.
/// </summary>
public class Promocode
{
    /// <summary>
    /// Идентификатор промокода и сам промокод.
    /// </summary>
    public required string Code { get; set; }

    /// <summary>
    /// Идентификатор владельца промокода.
    /// </summary>
    public Guid? OwnerId { get; set; }

    /// <summary>
    /// Владелец промокода.
    /// </summary>
    public User? Owner { get; set; }

    /// <summary>
    /// Идентификатор приза.
    /// </summary>
    public Guid? PrizeId { get; set; }

    /// <summary>
    /// Приз.
    /// </summary>
    public Prize? Prize { get; set; }
}