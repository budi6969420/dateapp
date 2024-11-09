// ------------------------------------------------------------------
// © Copyright 2024 Thermo Fisher Scientific Inc. All rights reserved.
// ------------------------------------------------------------------
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DateAppApi.IServices;
using DateAppApi.Models;
using Microsoft.IdentityModel.Tokens;

namespace DateAppApi.Services;

public class JwtService : IJwtService
{
    public JwtService(IConfiguration configuration)
    {
        m_configuration = configuration;
        m_key = configuration["Jwt:Key"];
        m_issuer = configuration["Jwt:Issuer"];
        m_audience = configuration["Jwt:Audience"];
    }

    #region IJwtService Members
    public int GetUserIdFromToken(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = m_issuer,
            ValidAudience = m_audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(m_key))
        };

        var principal = handler.ValidateToken(token, validationParameters, out var securityToken);
        var stringId = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value!;
        return int.Parse(stringId);
    }

    public string GenerateJwtToken(User user)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(m_configuration["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var tokenDescriptor = new JwtSecurityToken(
            issuer: m_configuration["Jwt:Issuer"],
            audience: m_configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddHours(10),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
    }
    #endregion

    #region private fields and constants
    private readonly string m_audience;
    private readonly IConfiguration m_configuration;
    private readonly string m_issuer;
    private readonly string m_key;
    #endregion
}