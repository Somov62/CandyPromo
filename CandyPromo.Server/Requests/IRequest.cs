namespace CandyPromo.Server.Requests;

/// <summary>
/// Контракт для dto запроса, открывающий
/// для неё возможность автоматической валидации перед попаданием в контроллер.
/// </summary>
public interface IRequest
{
    /// <summary>
    /// Производит валидацию модели запроса.
    /// </summary>
    void Validate(ValidationSession session);
}
