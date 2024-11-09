// ------------------------------------------------------------------
// © Copyright 2024 Thermo Fisher Scientific Inc. All rights reserved.
// ------------------------------------------------------------------
using DateAppApi.DbContexts;
using DateAppApi.IServices;
using DateAppApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace DateAppApi.Core
{
    public class AuthController : ControllerBase
    {
        public AuthController(IJwtService jwtService, AppDbContext context)
        {
            m_jwtService = jwtService;
            m_context = context;
        }

        protected bool TryAuthenticate(out User? user)
        {
            user = null;

            if (!TryGetToken(out var token)) return false;

            var id = m_jwtService.GetUserIdFromToken(token);
            user = m_context.Users.Find(id);

            return user != null;
        }

        private bool TryGetToken(out string token)
        {
            token = null!;
            var authHeader = Request.Headers["Authorization"].FirstOrDefault();
            if (authHeader == null || !authHeader.StartsWith("Bearer ")) return false;

            token = authHeader.Substring("Bearer ".Length).Trim();
            return true;
        }

        #region private fields and constants
        private readonly AppDbContext m_context;
        private readonly IJwtService m_jwtService;
        #endregion
    }
}