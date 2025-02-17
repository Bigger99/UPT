using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using UPT.SignalR.Models;

namespace UPT.SignalR.Hubs;

public class ChatHub(IMemoryCache cache, ILogger<ChatHub> logger) : Hub<IChatClient>
{
    public async Task JoinChat(UserConnection connection)
    {
        var connectionId = Context.ConnectionId;
        await Groups.AddToGroupAsync(connectionId, connection.ChatRoom);

        var stringConnection = JsonSerializer.Serialize(connection);
        cache.Set(connectionId, stringConnection);

    //    await Clients
    //.Group(connection.ChatRoom)
    //.ReceiveMessage("Admin_bot", $"{connection.UserName} присоединился к чату");
    }

    public async Task SendMessage(string message)
    {
        if (!TryGetCachedConnection(out var connection))
        {
            return;
        }

        if (connection is not null)
        {
            await Clients
            .Group(connection.ChatRoom)
            .ReceiveMessage(connection.UserName, message);
        }
    }

    public override async Task OnDisconnectedAsync(Exception exception)
    {
        if (TryGetCachedConnection(out var connection))
        {
            var connectionId = Context.ConnectionId;
            cache.Remove(connectionId);
            await Groups.RemoveFromGroupAsync(connectionId, connection.ChatRoom);

            //await Clients
            //    .Group(connection.ChatRoom)
            //    .ReceiveMessage("Admin_bot", $"{connection.UserName} вышел из чата");
        }

        await base.OnDisconnectedAsync(exception);
    }

    private bool TryGetCachedConnection([NotNullWhen(true)]out UserConnection? connection)
    {
        connection = null;

        var cachedConnection = cache.Get(Context.ConnectionId);
        var stringConnection = cachedConnection?.ToString();

        if (string.IsNullOrEmpty(stringConnection))
        {
            logger.LogWarning($"Cached value not found. ConnectionId: {Context.ConnectionId}");
            return false;
        }

        connection = JsonSerializer.Deserialize<UserConnection>(stringConnection);

        if (connection is null)
        {
            return false;
        }

        return true;
    }
}