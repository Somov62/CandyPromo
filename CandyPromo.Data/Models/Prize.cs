using System.Text.Json.Serialization;

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
    public string? ImageUrl { get; set; }

    /// <summary>
    /// Описание приза.
    /// </summary>
    public string? Description { get; set; }

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
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum PrizeDeliveryStatus
{
    /// <summary>
    /// Промоакция не завершена.
    /// </summary>
    [JsonStringEnumMemberName("Промоакция не завершена")]
    PromotionNotEnded,

    /// <summary>
    /// Победитель найден.
    /// </summary>
    [JsonStringEnumMemberName("Победитель найден")]
    WinnerFound,

    /// <summary>
    /// Адрес победителя получен.
    /// </summary>
    [JsonStringEnumMemberName("Адрес победителя получен")]
    WinnerAddressReceived,

    /// <summary>
    /// Приз отправлен.
    /// </summary>
    [JsonStringEnumMemberName("Приз отправлен")]
    Sent,

    /// <summary>
    /// Получены данные победителя для доставки.
    /// </summary>
    [JsonStringEnumMemberName("Получены данные победителя для доставки")]
    FailedContactWinner,

    /// <summary>
    /// Приз доставлен.
    /// </summary>
    [JsonStringEnumMemberName("Приз доставлен")]
    Delivered
}