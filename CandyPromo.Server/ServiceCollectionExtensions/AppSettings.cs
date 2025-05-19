namespace CandyPromo.Server.ServiceCollectionExtensions;

/// <summary>
/// Класс настроек приложения, описанных в appsettings.json.
/// </summary>
public class AppSettings
{
    /// <summary>
    /// Дата завершения акции (дата розыгрыша).
    /// </summary>
    public DateTime PromoEndingDate { get; init; }
}
