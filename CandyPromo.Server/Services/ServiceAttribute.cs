namespace CandyPromo.Server.Services;

/// <summary>
/// Атрибут для класса, который назначают на сервисы.
/// Это нужно, чтобы не описывать каждый сервис в DI.
/// <see cref="ServiceLayer"/>
/// </summary>
[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
public sealed class ServiceAttribute : Attribute
{

}