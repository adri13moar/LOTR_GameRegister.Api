using LOTR_GameRegister.Application.Services.Implementations;
using LOTR_GameRegister.Application.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace LOTR_GameRegister.Application;

/// <summary>
/// Registers the application layer services (business logic) with the DI container.
/// </summary>
public static class AddApplicationExtensions
{
    /// <summary>
    /// Adds all application-layer services to the service collection.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add services to.</param>
    /// <returns>The same service collection so calls can be chained.</returns>
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<ICycleService, CycleService>();
        services.AddScoped<IDifficultyService, DifficultyService>();
        services.AddScoped<IGameService, GameService>();
        services.AddScoped<IHeroService, HeroService>();
        services.AddScoped<IQuestService, QuestService>();
        services.AddScoped<IReasonForDefeatService, ReasonForDefeatService>();
        services.AddScoped<IResultService, ResultService>();
        services.AddScoped<ISphereService, SphereService>();
        services.AddScoped<IUserService, UserService>();

        return services;
    }
}
