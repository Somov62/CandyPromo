using CandyPromo.Data.Models;
using CandyPromo.Server.Auth;
using CandyPromo.Server.Helpers;
using CandyPromo.Server.Requests;
using CandyPromo.Server.Requests.Validation;
using Microsoft.EntityFrameworkCore;

namespace CandyPromo.Server.Services;

/// <summary>
/// Сервис аутентификации и регистрации/
/// </summary>
[Service]
public class AuthService(CandyPromoContext database, JwtTokenGenerator tokenGenerator)
{
    /// <summary>
    /// Зайти в аккаунт (получить токен).
    /// </summary>
    public async Task<string> Login(LoginRequest request, CancellationToken cancel)
    {
        User? user = null;
        if (!string.IsNullOrEmpty(request.Email))
        {
            user = await database.Users.SingleOrDefaultAsync(u =>
            u.Email != null &&
            u.Email == request.Email.ToLower(), 
            cancel);
        }

        if (user == null && !string.IsNullOrEmpty(request.Phone))
        {
            user = await database.Users.SingleOrDefaultAsync(u => u.Phone == request.Phone, cancel);
            if (user == null)
                throw new ValidationException(new ValidationError(nameof(LoginRequest.Password), "Пользователь не найден."));
        }

        if (user == null)
            throw new ValidationException(new ValidationError(nameof(LoginRequest.Email), "Пользователь не найден."));

        if (!PasswordHasher.Verify(request.Password, user.Password))
            throw new ValidationException(new ValidationError(nameof(LoginRequest.Password), "Неправильный пароль."));

        return tokenGenerator.Generate(user.Id, user.IsAdmin);
    }

    /// <summary>
    /// Регистрация нового пользователя.
    /// </summary>
    public async Task<Guid> Register(RegisterUserRequest request, CancellationToken cancel)
    {
        var hashedPassword = PasswordHasher.Generate(request.Password);

        var user = new User()
        {
            Name = request.Name,
            Email = request.Email?.ToLower(),
            Phone = request.Phone?.ToLower(),
            Password = hashedPassword
        };

        if (await database.Users.AnyAsync(u => u.Email == user.Email || u.Phone == user.Phone, cancel))
            throw new ArgumentException("Пользователь с такими данными уже зарегистрирован.");

        await database.Users.AddAsync(user, cancel);
        await database.SaveChangesAsync(cancel);

        return user.Id;
    }
}