﻿using Microsoft.EntityFrameworkCore;
using UPT.Data;
using UPT.Domain.Entities;
using UPT.Infrastructure.Jwt;
using UPT.Infrastructure.PasswordHasher;

namespace UPT.Features.Services.Autorization;

public class AutorizationService(
    IPasswordHasher passwordHasher,
    UPTDbContext dbContext,
    IJwtProvider jwtProvider) : IAutorizationService
{
    public async Task Register(string email, string password)
    {
        var hashedPassword = passwordHasher.Generate(password);
        var user = new User(email, hashedPassword);
        await dbContext.Users.AddAsync(user);
        await dbContext.SaveChangesAsync();
    }

    public async Task<string> Login(string email, string password)
    {
        var existedUser = await dbContext.Users.FirstOrDefaultAsync(x => x.EmailAddress == email)
            ?? throw new InvalidOperationException("User not found");

        var isValid = passwordHasher.Verify(password, existedUser.PasswordHash);

        if (!isValid)
        {
            throw new InvalidOperationException("Password is not correct");
        }

        var token = jwtProvider.GenerateToken(existedUser.Id);
        return token;
    }
}
