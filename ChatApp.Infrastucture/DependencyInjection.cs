using System.Text;
using ChatApp.Core.Interfaces;
using ChatApp.Core.Models;
using ChatApp.Infrastucture.Data;
using ChatApp.Infrastucture.Repositories;
using ChatApp.Infrastucture.SignalR.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace ChatApp.Infrastucture;

public static class DependencyInjection {
    public static IServiceCollection AddInfrastuctureDI(this IServiceCollection services, IConfiguration configuration) {
        services.AddDbContext<ApplicationDbContext>(options => {
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("ChatApp.Infrastucture"));
        });
 

        services.AddAuthentication(opt => {
            opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(otp => {
            otp.TokenValidationParameters = new TokenValidationParameters {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidateLifetime = true,
                ValidIssuer = configuration["Jwt:Issuer"],
                ValidAudience = configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!))
            };
        });
        
        services.AddScoped<IUserRepository,UserRepository>();
        services.AddScoped<IRoomRepository,RoomRepository>();
        services.AddScoped<IMessageRepository,MessageRepository>();
        services.AddScoped<JwtService>();

        return services;
    }
    
    
}