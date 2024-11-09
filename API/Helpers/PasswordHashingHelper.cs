// ------------------------------------------------------------------
// © Copyright 2024 Thermo Fisher Scientific Inc. All rights reserved.
// ------------------------------------------------------------------
namespace DateAppApi.Helpers
{
    public static class PasswordHashingHelper
    {
        public static string HashPassword(string password) => BCrypt.Net.BCrypt.HashPassword(password);
    }
}