namespace CandyPromo.Server.Controllers;

/// <summary>
/// Контроллер для призов.
/// </summary>
public class PrizesController(PrizeService service) : BaseController
{
    /// <summary>
    /// Возвращает краткую информацию о призах.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(200, Type = typeof(List<PrizeSummaryResponse>))]
    public async Task<IActionResult> GetPrizesSummary(CancellationToken cancel)
    {
        var prizes = await service.GetPrizesSummary(cancel);

        return Ok(prizes);
    }

    /// <summary>
    /// Возвращает полную информацию о призах.
    /// Включает в себя информацию о победителе.
    /// </summary>
    [HttpGet("details")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(200, Type = typeof(List<PrizeFullDetailsResponse>))]
    public async Task<IActionResult> GetPrizesFullDetails(CancellationToken cancel)
    {
        var prizes = await service.GetPrizesFullDetails(cancel);

        return Ok(prizes);
    }

    /// <summary>
    /// Возвращает полную информацию о призах.
    /// Включает в себя информацию о победителе.
    /// </summary>
    [HttpGet("{prizeId}/details")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(200, Type = typeof(PrizeFullDetailsResponse))]
    public async Task<IActionResult> GetPrizeFullDetailsById([FromRoute] Guid prizeId, CancellationToken cancel)
    {
        var prize = await service.GetPrizeFullDetails(prizeId, cancel);

        return Ok(prize);
    }

    /// <summary>
    /// Возвращает контактные данные победителя указанного приза.
    /// </summary>
    [HttpGet("{prizeId}/contacts")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(200, Type = typeof(WinnerContactsResponse))]
    public async Task<IActionResult> GetWinnerContacts([FromRoute] Guid prizeId, CancellationToken cancel)
    {
        var contacts = await service.GetWinnerContacts(prizeId, cancel);
        return Ok(contacts);
    }

    /// <summary>
    /// Редактирование статуса вручения приза.
    /// </summary>
    /// <param name="prizeId"> Идентификатор приза </param>
    /// <param name="status"> Новый статус </param>
    /// <param name="cancel"></param>
    [HttpPut("{prizeId}/status")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(200)]
    public async Task<IActionResult> UpdatePrizeStatus(
        [FromRoute] Guid prizeId,
        [FromBody][Required] PrizeDeliveryStatus status,
        CancellationToken cancel)
    {
        await service.UpdatePrizeStatus(prizeId, status, cancel);

        return Ok();
    }

    /// <summary>
    /// Возвращает статусы приза
    /// </summary>
    [HttpGet("statuses")]
    [ProducesResponseType(200, Type = typeof(string[]))]
    [Authorize(Roles = "Admin")]
    public IActionResult GetPrizeStatuses()
    {
        return Ok(Enum.GetValues<PrizeDeliveryStatus>());
    }
}
