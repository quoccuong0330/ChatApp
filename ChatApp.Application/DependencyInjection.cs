using ChatApp.Application.Mapping;
using Microsoft.Extensions.DependencyInjection;

namespace ChatApp.Application;

public static class DependencyInjection {
    public static IServiceCollection AddAppDI(this IServiceCollection services) {
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));
        services.AddAutoMapper(typeof(MappingProfile).Assembly);
        return services;
    }
}