using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Azure.Core;
using ChatApp.Core.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace ChatApp.Infrastucture.SignalR.Services;

public class JwtService {
    private readonly IConfiguration _configuration;

    public JwtService(IConfiguration configuration) {
        _configuration = configuration;
    }
    
    
    public string CreateAssessToken(UserModel user) {
        var issuer = _configuration["Jwt:Issuer"];
        var audience = _configuration["Jwt:Audience"];
        var key = _configuration["Jwt:Key"];

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key!));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var jti = Guid.NewGuid().ToString();
        
        var clams = new[] {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.NameIdentifier, user.FirstName + user.LastName),
            new Claim(JwtRegisteredClaimNames.Jti, jti) 

        };
        
 

        var token = new JwtSecurityToken(
            issuer,
            audience,
            clams,
            expires: DateTime.UtcNow.AddMinutes(30),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);

    }

    public class RefreshToken() {
        public string Token { set; get; } = string.Empty;
        public DateTime Expired { set; get; }
    }
    
    public RefreshToken CreateRefreshToken()
    {
        var randomNumber = new byte[32];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomNumber);
        }
        return new RefreshToken{
            Token= Convert.ToBase64String(randomNumber),
            Expired = DateTime.UtcNow.AddDays(5),
        };
    }
}