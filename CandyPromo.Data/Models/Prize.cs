namespace CandyPromo.Data.Models;

public class Prize
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public required string Name { get; set; }
    public string? ImageName { get; set; }
    public string? Descripton { get; set; }
    public PrizeDeliveryStatus Status { get; set; } = PrizeDeliveryStatus.PromotionNotEnded;

    public string? PromocodeId { get; set; }
    public Promocode? Promocode { get; set; }
}

public enum PrizeDeliveryStatus
{
    PromotionNotEnded,
    WinnerFinding,
    WinnerAddressReceived,
    Sent,
    FailedContactWinner,
    Delivered
}