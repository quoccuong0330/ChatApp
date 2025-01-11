using ChatApp.Core.Interfaces;
using ChatApp.Core.Models;
using ChatApp.Infrastucture.Data;
using ChatApp.Infrastucture.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ChatApp.Infrastucture;

public static class DependencyInjection {
    public static IServiceCollection AddInfrastuctureDI(this IServiceCollection services, IConfiguration configuration) {
        services.AddDbContext<ApplicationDbContext>(options => {
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("ChatApp.Infrastucture"));
        });
        services.AddScoped<IUserRepository,UserRepository>();
        services.AddScoped<IRoomRepository,RoomRepository>();
        return services;
    }
}