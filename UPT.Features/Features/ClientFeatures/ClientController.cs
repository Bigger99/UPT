using Microsoft.AspNetCore.Mvc;
using UPT.Features.Base;
using UPT.Features.Features.ClientFeatures.Requests;
using UPT.Features.Services.Client;
using UPT.Infrastructure.Models;

namespace UPT.Features.Features.ClientFeatures;

/// <summary>
/// Контроллер для Client
/// </summary>
public class ClientController(IClientService clientService) : BaseAuthorizeController
{
    /// <summary>
    /// Получить клиента
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] int clientId)
    {
        var user = await clientService.Get(clientId);
        return Ok(user);
    }

    /// <summary>
    /// Получить клиента
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetByUserId([FromQuery] int userid)
    {
        var trainer = await clientService.GetByUserId(userid);
        return Ok(trainer);
    }

    /// <summary>
    /// Получить клиента
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> GetFiltered([FromBody] PagedFilterQuery<ClientRequest> request)
    {
        var trainer = await clientService.GetFilteredClients(request);
        return Ok(trainer);
    }

    /// <summary>
    /// Создать клиента
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateClientCommand command)
    {
        var trainer = await clientService.Create(command.UserId, command.TrainingPrograms, command.Height, command.Weight, command.VolumeBreast, command.VolumeWaist, command.VolumeAbdomen, command.VolumeButtock, command.VolumeHip);
        return Ok(trainer);
    }

    /// <summary>
    /// Изменить клиента
    /// </summary>
    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateClientCommand command)
    {
        var trainer = await clientService.Update(command.ClientId, command.TrainingPrograms, command.Height, command.Weight, command.VolumeBreast, command.VolumeWaist, command.VolumeAbdomen, command.VolumeButtock, command.VolumeHip);
        return Ok(trainer);
    }

    /// <summary>
    /// Установить тренера клиенту
    /// </summary>
    [HttpPut]
    public async Task<IActionResult> SetTrainer([FromBody] SetClientTrainerCommand command)
    {
        var trainer = await clientService.SetTrainer(command.ClientId, command.TrainerId);
        return Ok(trainer);
    }

    /// <summary>
    /// Удалить клиента
    /// </summary>
    [HttpDelete]
    public async Task<IActionResult> Delete([FromQuery] int clientId)
    {
        await clientService.Delete(clientId);
        return Ok();
    }
}
