// ------------------------------------------------------------------
// © Copyright 2024 Thermo Fisher Scientific Inc. All rights reserved.
// ------------------------------------------------------------------
using DateAppApi.Models;

namespace DateAppApi.IServices;

public interface IJwtService
{
    int GetUserIdFromToken(string token);
    string GenerateJwtToken(User user);
}