using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependenceInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = typeof(DependenceInjection).Assembly;

        services.AddMediatR(configuration =>
        configuration.RegisterServicesFromAssemblies(assembly));

        services.AddValidatorsFromAssembly(assembly);

        return services;
    }
}
