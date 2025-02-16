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
    /// Получить всех клиентов
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var user = await clientService.GetAll();
        return Ok(user);
    }

    /// <summary>
    /// Получить клиента
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetByUserId([FromQuery] int userid)
    {
        var client = await clientService.GetByUserId(userid);
        return Ok(client);
    }

    /// <summary>
    /// Получить клиента
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> GetFiltered([FromBody] PagedFilterQuery<FilteredClientRequest> request)
    {
        var client = await clientService.GetFilteredClients(request);
        return Ok(client);
    }

    /// <summary>
    /// Создать клиента
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateClientCommand command)
    {
        var client = await clientService.Create(command.UserId, command.Height, command.Weight, command.VolumeBreast, command.VolumeWaist, command.VolumeAbdomen, command.VolumeButtock, command.VolumeHip);
        return Ok(client);
    }

    /// <summary>
    /// Изменить клиента
    /// </summary>
    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateClientCommand command)
    {
        var client = await clientService.Update(command.ClientId, command.Height, command.Weight, command.VolumeBreast, command.VolumeWaist, command.VolumeAbdomen, command.VolumeButtock, command.VolumeHip);
        return Ok(client);
    }

    /// <summary>
    /// Установить тренера клиенту
    /// </summary>
    [HttpPut]
    public async Task<IActionResult> SetTrainer([FromBody] SetClientTrainerCommand command)
    {
        var client = await clientService.SetTrainer(command.ClientId, command.TrainerId);
        return Ok(client);
    }

    /// <summary>
    /// Получить тренера клиента
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetTrainer([FromQuery] int clientId)
    {
        var client = await clientService.GetTrainer(clientId);
        return Ok(client);
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
