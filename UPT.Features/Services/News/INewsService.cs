using UPT.Features.Features.NewsFeatures.Dto;

namespace UPT.Features.Services.News;

public interface INewsService
{
    Task<NewsDto> Create(string name, string text, int userId, string? image);
    Task Delete(int newsId);
    Task<NewsDto?> Get(int newsId);
    Task<List<NewsDto>?> GetAll();
    Task<NewsDto> Update(int newsId, string name, string text, int userId, string? image);
}