// ------------------------------------------------------------------
// © Copyright 2024 Thermo Fisher Scientific Inc. All rights reserved.
// ------------------------------------------------------------------
#pragma warning disable CS8618
namespace DateAppApi.Dtos
{
    public class TokenDto
    {
        public string Token { get; set; }
        public int ExpiresIn { get; } = (int)GlobalConstants.JwtLifeSpan.TotalSeconds;
    }
}