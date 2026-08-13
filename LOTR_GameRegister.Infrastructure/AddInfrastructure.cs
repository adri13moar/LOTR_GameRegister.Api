using Dapper;
using LOTR_GameRegister.Application.Repositories.Interfaces;
using LOTR_GameRegister.Application.Services.Interfaces;
using LOTR_GameRegister.Infrastructure.Handlers;
using LOTR_GameRegister.Infrastructure.Repositories.Implementations;
using LOTR_GameRegister.Infrastructure.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LOTR_GameRegister.Infrastructure;

/// <summary>
/// Registers the infrastructure layer services (data access, security, Dapper handlers) with the DI container.
/// </summary>
public static class AddInfrastructureExtensions
{
    /// <summary>
    /// Adds all infrastructure-layer services to the service collection.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add services to.</param>
    /// <param name="configuration">Application configuration used to build connection strings and JWT settings.</param>
    /// <returns>The same service collection so calls can be chained.</returns>
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        SqlMapper.AddTypeHandler(new DateOnlyTypeHandler());

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ICycleRepository, CycleRepository>();
        services.AddScoped<IDifficultyRepository, DifficultyRepository>();
        services.AddScoped<IGameRepository, GameRepository>();
        services.AddScoped<IHeroRepository, HeroRepository>();
        services.AddScoped<IQuestRepository, QuestRepository>();
        services.AddScoped<IReasonForDefeatRepository, ReasonForDefeatRepository>();
        services.AddScoped<IResultRepository, ResultRepository>();
        services.AddScoped<ISphereRepository, SphereRepository>();

        services.AddScoped<ITokenService, TokenService>();

        return services;
    }
}
