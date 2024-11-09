// ------------------------------------------------------------------
// © Copyright 2024 Thermo Fisher Scientific Inc. All rights reserved.
// ------------------------------------------------------------------
using DateAppApi.Core;
using DateAppApi.DbContexts;
using DateAppApi.Dtos.DateIdea;
using DateAppApi.IServices;
using Microsoft.AspNetCore.Mvc;

namespace DateAppApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class DateIdeaController : BaseAuthController
    {
        public DateIdeaController(IJwtService jwtService, AppDbContext context, IDateIdeaService dateIdeaService) : base(jwtService, context)
        {
            m_dateIdeaService = dateIdeaService;
        }

        [HttpGet]
        public async Task<IActionResult> GetDateIdea(int id)
        {
            if (!TryAuthenticate(out _)) return Unauthorized();
            var dateIdea = await m_dateIdeaService.GetDateIdeaAsync(id);
            if (dateIdea == null) return NotFound();
            return Ok(dateIdea.ToDto());
        }

        [HttpPost]
        public async Task<IActionResult> CreateDateIdea(DateIdeaAddDto dto)
        {
            if (!TryAuthenticate(out _)) return Unauthorized();
            var userId = GetId();
            var dateIdea = await m_dateIdeaService.CreateDateIdeaAsync(userId, dto.Description);
            return Ok(dateIdea.ToDto());
        }

        [HttpPut]
        public async Task<IActionResult> UpdateDateIdea(DateIdeaUpdateDto dto, int dateIdeaId)
        {
            if (!TryAuthenticate(out _)) return Unauthorized();
            var userId = GetId();
            var dateIdea = await m_dateIdeaService.UpdateDateIdea(userId, dateIdeaId, dto.Description);
            return Ok(dateIdea.ToDto());
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteDateIdea(int dateIdeaId)
        {
            if (!TryAuthenticate(out _)) return Unauthorized();
            var userId = GetId();
            await m_dateIdeaService.DeleteDateIdea(userId, dateIdeaId);
            return Ok();
        }

        #region private fields and constants
        private readonly IDateIdeaService m_dateIdeaService;
        #endregion
    }
}