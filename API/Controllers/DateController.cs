// ------------------------------------------------------------------
// © Copyright 2024 Thermo Fisher Scientific Inc. All rights reserved.
// ------------------------------------------------------------------
using DateAppApi.Core;
using DateAppApi.DbContexts;
using DateAppApi.Dtos.Date;
using DateAppApi.Helpers;
using DateAppApi.IServices;
using Microsoft.AspNetCore.Mvc;

namespace DateAppApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class DateController : BaseAuthController
    {
        public DateController(IJwtService jwtService, AppDbContext context, IDateService dateService) : base(jwtService, context)
        {
            m_dateService = dateService;
        }

        [HttpGet]
        public async Task<IActionResult> GetDate(int id)
        {
            if (!TryAuthenticate(out _)) return Unauthorized();
            var dateIdea = await m_dateService.GetDateAsync(id);
            if (dateIdea == null) return NotFound();
            return Ok(dateIdea.ToDto());
        }

        [HttpPost]
        public async Task<IActionResult> CreateDate(DateAddDto dto)
        {
            if (!TryAuthenticate(out _)) return Unauthorized();
            var userId = GetId();
            var dateIdea = await m_dateService.CreateDateAsync(userId,
                dto.OtherUserId,
                dto.Description,
                dto.DateStartDate,
                dto.DateEndDate,
                dto.DateIdeaIds,
                dto.ImageDatas);
            return Ok(dateIdea.ToDto());
        }

        [HttpPut]
        public async Task<IActionResult> UpdateDate(DateUpdateDto dto, int dateId)
        {
            if (!TryAuthenticate(out _)) return Unauthorized();
            var userId = GetId();
            var dateIdea = await m_dateService.UpdateDateAsync(userId, dateId, dto.Description, dto.DateStartDate, dto.DateEndDate);
            return Ok(dateIdea.ToDto());
        }

        [HttpPost("{dateId}/image")]
        public async Task<IActionResult> AddImageToDate(int dateId, IFormFile file)
        {
            if (!TryAuthenticate(out _)) return Unauthorized();
            var userId = GetId();
            var image = await FormFileHelper.ToByteArrayAsync(file);
            await m_dateService.AddImageToDateAsync(dateId, userId, image);
            return Ok();
        }

        [HttpDelete("{dateId}/image/{imageId}")]
        public async Task<IActionResult> RemoveImageFromDate(int dateId, int imageId)
        {
            if (!TryAuthenticate(out _)) return Unauthorized();
            var userId = GetId();
            await m_dateService.RemoveImageFromDateAsync(dateId, userId, imageId);
            return Ok();
        }

        [HttpPost("{dateId}/idea/{dateIdeaId}")]
        public async Task<IActionResult> AddDateIdeaToDate(int dateId, int dateIdeaId)
        {
            if (!TryAuthenticate(out _)) return Unauthorized();
            var userId = GetId();
            await m_dateService.AddDateIdeaToDateAsync(dateId, userId, dateIdeaId);
            return Ok();
        }

        [HttpDelete("{dateId}/idea/{imageId}")]
        public async Task<IActionResult> RemoveDateIdeaFromDate(int dateId, int dateIdeaId)
        {
            if (!TryAuthenticate(out _)) return Unauthorized();
            var userId = GetId();
            await m_dateService.RemoveDateIdeaFromDateAsync(dateId, userId, dateIdeaId);
            return Ok();
        }

        #region private fields and constants
        private readonly IDateService m_dateService;
        #endregion
    }
}