// ------------------------------------------------------------------
// © Copyright 2024 Thermo Fisher Scientific Inc. All rights reserved.
// ------------------------------------------------------------------
using DateAppApi.Core;
using DateAppApi.DbContexts;
using DateAppApi.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DateAppApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ImageController : BaseAuthController
    {
        public ImageController(IJwtService jwtService, AppDbContext context) : base(jwtService, context)
        {
            m_context = context;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (!TryAuthenticate(out _)) return Unauthorized();
            var image = await m_context.Images.FindAsync(id);
            if (image == null) return NotFound();
            return File(image.Data, "image/jpeg");
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetFromUser(int userId)
        {
            if (!TryAuthenticate(out _)) return Unauthorized();
            var user = await m_context.Users
                .Include(x => x.ProfilePicture)
                .FirstOrDefaultAsync(x => x.Id == userId);
            if (user == null || user.ProfilePicture == null) return NotFound();
            return File(user.ProfilePicture.Data, "image/jpeg");
        }

        #region private fields and constants
        private readonly AppDbContext m_context;
        #endregion
    }
}