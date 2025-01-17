using ChatApp.Application;
using ChatApp.Application.Services;
using ChatApp.Core;
using ChatApp.Core.Interfaces;
using ChatApp.Core.Models;
using ChatApp.Infrastucture;
using ChatApp.Presentation;
using ChatApp.Presentation.Hubs;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.SignalR;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
});

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
    });



builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("SignalRCorsPolicy", builder =>
    {
        builder.WithOrigins("http://localhost:5287") // Chỉ định domain
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});


 
builder.Services.AddSignalR(otp => {
    otp.EnableDetailedErrors = true;    
    otp.KeepAliveInterval = TimeSpan.FromSeconds(10); 
    otp.HandshakeTimeout = TimeSpan.FromSeconds(5);
});
builder.Services.AddCoreDI();
builder.Services.AddAppDI();
builder.Services.AddInfrastuctureDI(builder.Configuration);
builder.Services.AddSignalR(options =>
{
    options.EnableDetailedErrors = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.MapPost("chat", async (string message, IHubContext<ChatHub, INotificationService> context) => {
//     await context.Clients.All.ReceiveMessage(message);
//     return Results.NoContent();
// });
app.UseRouting();
app.UseCors("SignalRCorsPolicy"); // Đảm bảo policy được áp dụng trước SignalR
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapHub<ChatHub>("/chat-hub");
app.Run();