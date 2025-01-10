using Microsoft.Extensions.DependencyInjection;

namespace ChatApp.Core;

public static class DependencyInjection {
    public static IServiceCollection AddCoreDI(this IServiceCollection services) {
        return services;
    }
}