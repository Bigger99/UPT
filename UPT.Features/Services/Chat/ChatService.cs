using Mapster;
using Microsoft.EntityFrameworkCore;
using UPT.Data;
using UPT.Features.Features.ChatFeatures.Dto;
using UPT.Infrastructure.Middlewars;

namespace UPT.Features.Services.Chat;

public class ChatService(UPTDbContext dbContext) : IChatService
{
    public async Task<List<ChatDto>?> GetHistory(int senderId, int recipientId)
    {
        var sender = await dbContext.Users
            .FirstOrDefaultAsync(x => x.Id == senderId) ?? throw new BackendException("Sender not found");

        var recipient = await dbContext.Users
            .FirstOrDefaultAsync(x => x.Id == recipientId) ?? throw new BackendException("Recipient not found");

        var history = await dbContext.Chats
            .Where(x => x.Sender.Id == senderId || x.Sender.Id == recipientId)
            .Where(x => x.Recipient.Id == senderId || x.Recipient.Id == recipientId)
            .OrderBy(x => x.Time)
            .Select(x => x.Adapt<ChatDto>())
            .ToListAsync();

        return history;
    }

    public async Task Add(int senderId, int recipientId, string message)
    {
        var sender = await dbContext.Users
            .FirstOrDefaultAsync(x => x.Id == senderId) ?? throw new BackendException("Sender not found");

        var recipient = await dbContext.Users
            .FirstOrDefaultAsync(x => x.Id == recipientId) ?? throw new BackendException("Recipient not found");

        var chatMessage = new Domain.Entities.Chat(sender, recipient, message, DateTime.UtcNow);
        await dbContext.Chats.AddAsync(chatMessage);

        var newNotification = new Domain.Entities.Notification(string.Empty, DateTime.UtcNow, $"Вам прислал сообщение пользователь: {sender.Name}", recipient);
        await dbContext.Notifications.AddAsync(newNotification);

        await dbContext.SaveChangesAsync();
    }

    public async Task Delete(int messageId)
    {
        var message = await dbContext.Chats
            .FirstOrDefaultAsync(x => x.Id == messageId) ?? throw new BackendException("Message not found"); ;

        dbContext.Chats.Remove(message);
        await dbContext.SaveChangesAsync();
    }
}
