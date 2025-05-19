namespace CandyPromo.Server.ServiceCollectionExtensions;

/// <summary>
/// Расширение для <see cref="IServiceCollection"/>
/// для регистрации всех сервисов одним методом.
/// </summary>
public static class ServiceLayer
{
    /// <summary>
    /// Регистрация слоя сервисов.
    /// </summary>
    /// <remarks>
    /// Чтобы сервис был зарегистрирован, его класс
    /// должен быть помечен атрибутом <see cref="ServiceAttribute"/>
    /// </remarks>
    public static IServiceCollection AddServicesLayer(this IServiceCollection collection)
    {
        foreach (var service in GetServices())
            collection.AddScoped(service);

        return collection;
    }

    /// <summary>
    /// Загружает все сервисы в виде списка.
    /// </summary>
    private static IEnumerable<Type> GetServices()
    {
        return Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(t => t.GetCustomAttribute<ServiceAttribute>() != null);
    }
}
