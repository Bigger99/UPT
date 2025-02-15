using UPT.Features.Features.ChatFeatures.Dto;

namespace UPT.Features.Services.Chat;

public interface IChatService
{
    Task Add(int senderId, int recipientId, string message);
    Task Delete(int messageId);
    Task<List<ChatDto>?> GetHistory(int senderId, int recipientId);
}