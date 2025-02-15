using Microsoft.EntityFrameworkCore;
using UPT.Data;
using UPT.Infrastructure.Email;
using UPT.Infrastructure.Email.Service;
using UPT.Infrastructure.Jwt;
using UPT.Infrastructure.Middlewars;
using UPT.Infrastructure.PasswordHasher;

namespace UPT.Features.Services.Autorization;

public class AutorizationService(
    IPasswordHasher passwordHasher,
    UPTDbContext dbContext,
    IEmailService emailService,
    IJwtProvider jwtProvider) : IAutorizationService
{
    public async Task Register(string email, string password)
    {
        var emailAddressLower = email.ToLower();

        var existedUser = await dbContext.Users
            .AnyAsync(x => x.EmailAddress.ToLower() == emailAddressLower);

        if (existedUser)
        {
            throw new BackendException("User with current email already existed");
        }

        var hashedPassword = passwordHasher.Generate(password);
        var user = new Domain.Entities.User(email, hashedPassword);
        await dbContext.Users.AddAsync(user);
        await dbContext.SaveChangesAsync();

        var emailMetadata = new EmailMetadata(email, "Регистрация в сервисе UPT", "Вы успешно зарегистрированы");
        await emailService.Send(emailMetadata);
    }

    public async Task<TokensModel> Login(string email, string password)
    {
        var existedUser = await dbContext.Users
            .AsNoTracking()
            .Where(x => !x.IsDeleted)
            .FirstOrDefaultAsync(x => x.EmailAddress == email)
            ?? throw new BackendException("User not found");

        var isValid = passwordHasher.Verify(password, existedUser.PasswordHash);

        if (!isValid)
        {
            throw new BackendException("Password is not correct");
        }

        var tokens = jwtProvider.GenerateTokens(existedUser.Id);
        return tokens;
    }    
    
    public Task<string> RefreshAccessToken(string refreshToken)
    {
        var token = jwtProvider.RefreshAccessToken(refreshToken);
        return Task.FromResult(token);
    }

    public async Task EditPassword(string email, string oldPassword, string newPassword)
    {
        var existedUser = await dbContext.Users.FirstOrDefaultAsync(x => x.EmailAddress == email)
            ?? throw new BackendException("User not found");

        var isValid = passwordHasher.Verify(oldPassword, existedUser.PasswordHash);

        if (!isValid)
        {
            throw new BackendException("Current password is not correct");
        }

        var hashedPassword = passwordHasher.Generate(newPassword);
        existedUser.EditPasswordHash(hashedPassword);

        var emailMetadata = new EmailMetadata(email, "Изменение пароля для сервиса UPT", "Ваш пароль был изменен");
        await emailService.Send(emailMetadata);

        await dbContext.SaveChangesAsync();
    }

    public async Task RestorePassword(string email)
    {
        var existedUser = await dbContext.Users.FirstOrDefaultAsync(x => x.EmailAddress == email)
            ?? throw new BackendException("User not found");

        var newPasword = Guid.NewGuid().ToString();
        var hashedPassword = passwordHasher.Generate(newPasword);
        existedUser.EditPasswordHash(hashedPassword);

        var emailMetadata = new EmailMetadata(email, "Запрос на восстановление пароля для сервиса UPT", $"Ваш новый пароль: {newPasword}");
        await emailService.Send(emailMetadata);

        await dbContext.SaveChangesAsync();
    }
}
