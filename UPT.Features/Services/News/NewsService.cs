﻿using Mapster;
using Microsoft.EntityFrameworkCore;
using System.Web;
using UPT.Data;
using UPT.Features.Features.NewsFeatures.Dto;
using UPT.Infrastructure.Middlewars;

namespace UPT.Features.Services.News;

public class NewsService(UPTDbContext dbContext) : INewsService
{
    public async Task<NewsDto?> Get(int newsId)
    {
        var news = await dbContext.News
            .AsNoTrackingWithIdentityResolution()
            .Include(x => x.User)
                .ThenInclude(x => x.City)
            .FirstOrDefaultAsync(x => x.Id == newsId);

        if (news is null)
        {
            return null;
        }

        return news.Adapt<NewsDto>();
    }

    public async Task<List<NewsDto>?> GetAll()
    {
        var news = await dbContext.News
            .AsNoTrackingWithIdentityResolution()
            .Include(x => x.User)
                .ThenInclude(x => x.City)
            .ToListAsync();

        return news.Select(x => x.Adapt<NewsDto>()).ToList();
    }

    public async Task<NewsDto> Create(string name, string text, int userId, string? image)
    {
        var user = await dbContext.Users
            .FirstOrDefaultAsync(x => x.Id == userId) ?? throw new BackendException("User not found");

        var news = new Domain.Entities.News(name, DateTime.UtcNow, HttpUtility.HtmlEncode(text), user, image);
        await dbContext.News.AddAsync(news);
        await dbContext.SaveChangesAsync();

        return news.Adapt<NewsDto>();
    }

    public async Task<NewsDto> Update(int newsId, string name, string text, int userId, string? image)
    {
        var news = await dbContext.News
            .FirstOrDefaultAsync(x => x.Id == newsId) ?? throw new BackendException("News not found");

        var user = await dbContext.Users
            .FirstOrDefaultAsync(x => x.Id == userId) ?? throw new BackendException("User not found");

        news.UpdateNews(name, DateTime.UtcNow, HttpUtility.HtmlEncode(text), user, image);
        await dbContext.SaveChangesAsync();

        return news.Adapt<NewsDto>();
    }

    public async Task Delete(int newsId)
    {
        var news = await dbContext.News
            .FirstOrDefaultAsync(x => x.Id == newsId);

        if (news is null)
        {
            return;
        }

        dbContext.News.Remove(news);
        await dbContext.SaveChangesAsync();
    }
}
