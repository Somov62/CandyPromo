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
    public async Task<TokenResult> Login(LoginRequest request, CancellationToken cancel)
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
                throw new ValidationException("Пользователь не найден.", nameof(LoginRequest.Phone));
        }

        if (user == null)
            throw new ValidationException("Пользователь не найден.", nameof(LoginRequest.Email));

        if (!PasswordHasher.Verify(request.Password, user.Password))
            throw new ValidationException("Неправильный пароль.", nameof(LoginRequest.Password));

        return new()
        {
            Token = tokenGenerator.Generate(user.Id, user.IsAdmin, user.Name),
            IsAdmin = user.IsAdmin
        };
    }

    /// <summary>
    /// Регистрация нового пользователя.
    /// </summary>
    /// <exception cref="ValidationException"></exception>
    public async Task<TokenResult> Register(RegisterUserRequest request, CancellationToken cancel)
    {
        var hashedPassword = PasswordHasher.Generate(request.Password);

        var user = new User()
        {
            Name = request.Name,
            Email = request.Email?.ToLower(),
            Phone = request.Phone?.ToLower(),
            Password = hashedPassword
        };

        if (await database.Users.AnyAsync(u =>
            (user.Email != null && u.Email == user.Email) ||
            (user.Phone != null && u.Phone == user.Phone),
            cancel))
        {
            if (user.Email != null && user.Phone == null)
                throw new ValidationException("Email уже зарегистрирован", nameof(user.Email));

            if (user.Phone != null && user.Email == null)
                throw new ValidationException("Телефон уже зарегистрирован", nameof(user.Phone));

            throw new ValidationException("Пользователь с такими данными уже зарегистрирован", nameof(user.Phone), nameof(user.Phone));
        }

        await database.Users.AddAsync(user, cancel);
        await database.SaveChangesAsync(cancel);

        return new()
        {
            Token = tokenGenerator.Generate(user.Id, false, user.Name)
        };
    }
}