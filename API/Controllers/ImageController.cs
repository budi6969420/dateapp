// ------------------------------------------------------------------
// © Copyright 2024 Thermo Fisher Scientific Inc. All rights reserved.
// ------------------------------------------------------------------
using DateAppApi.Core;
using DateAppApi.DbContexts;
using DateAppApi.IServices;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet]
        public async Task<IActionResult> Get(int id)
        {
            if (!TryAuthenticate(out _)) return Unauthorized();
            var image = await m_context.Images.FindAsync(id);
            if (image == null) return NotFound();
            return File(image.Data, "image/jpeg");
        }

        #region private fields and constants
        private readonly AppDbContext m_context;
        #endregion
    }
}