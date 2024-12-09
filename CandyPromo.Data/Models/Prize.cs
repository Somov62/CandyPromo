namespace CandyPromo.Data.Models;

/// <summary>
/// Приз.
/// </summary>
public class Prize
{
    /// <summary>
    /// Первичный ключ.
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Наименование приза.
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Путь к изображению приза.
    /// </summary>
    public string? ImageName { get; set; }

    /// <summary>
    /// Описание приза.
    /// </summary>
    public string? Descripton { get; set; }

    /// <summary>
    /// Статус приза.
    /// </summary>
    public PrizeDeliveryStatus Status { get; set; } = PrizeDeliveryStatus.PromotionNotEnded;

    /// <summary>
    /// Идентификатор промокода.
    /// </summary>
    public string? PromocodeId { get; set; }

    /// <summary>
    /// Промокод.
    /// </summary>
    public Promocode? Promocode { get; set; }
}

/// <summary>
/// Статусы приза.
/// </summary>
public enum PrizeDeliveryStatus
{
    /// <summary>
    /// Промоакция не завершена.
    /// </summary>
    PromotionNotEnded,
    /// <summary>
    /// Победитель найден.
    /// </summary>
    WinnerFinding,
    /// <summary>
    /// Адрес победителя получен.
    /// </summary>
    WinnerAddressReceived,
    /// <summary>
    /// Приз отправлен.
    /// </summary>
    Sent,
    /// <summary>
    /// Получены данные победителя для доставки.
    /// </summary>
    FailedContactWinner,
    /// <summary>
    /// Приз доставлен.
    /// </summary>
    Delivered
}