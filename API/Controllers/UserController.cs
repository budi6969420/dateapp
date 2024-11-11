// ------------------------------------------------------------------
// © Copyright 2024 Thermo Fisher Scientific Inc. All rights reserved.
// ------------------------------------------------------------------
using DateAppApi.Core;
using DateAppApi.DbContexts;
using DateAppApi.Dtos;
using DateAppApi.Dtos.User;
using DateAppApi.Helpers;
using DateAppApi.IServices;
using Microsoft.AspNetCore.Mvc;

namespace DateAppApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UserController : BaseAuthController
    {
        public UserController(IJwtService jwtService,
            AppDbContext context,
            IUserService userService) : base(jwtService, context)
        {
            m_jwtService = jwtService;
            m_userService = userService;
        }

        [HttpGet("self")]
        public async Task<IActionResult> GetSelf()
        {
            if (!TryAuthenticate(out _)) return Unauthorized();
            var id = GetId();
            var user = await m_userService.GetUserAsync(id);
            if (user == null) return NotFound();
            return Ok(user.ToDto());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (!TryAuthenticate(out _)) return Unauthorized();
            var user = await m_userService.GetUserAsync(id);
            if (user == null) return NotFound();
            return Ok(user.ToDto());
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            if (!TryAuthenticate(out _)) return Unauthorized();
            var users = m_userService.GetAllUsers();
            return Ok(users.Select(x => x.ToDto()));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var user = await m_userService.EnsureLoginOkAsync(loginDto.Username, loginDto.Password);
            var token = m_jwtService.GenerateJwtToken(user);
            return Ok(new TokenDto()
            {
                Token = token
            });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            var user = await m_userService.RegisterAsync(registerDto.Username, registerDto.Password, registerDto.Gender);
            var token = m_jwtService.GenerateJwtToken(user);
            return Ok(new TokenDto()
            {
                Token = token
            });
        }

        [HttpPost("image/{userId}")]
        public async Task<IActionResult> AddProfilePicture(int userId, IFormFile file)
        {
            if (!TryAuthenticate(out _)) return Unauthorized();
            if (GetId() != userId) return Unauthorized();
            var imageData = await FormFileHelper.ToByteArrayAsync(file);
            await m_userService.AddNewProfilePictureAsync(userId, imageData);
            return Ok();
        }

        [HttpDelete("image/{userId}")]
        public async Task<IActionResult> RemoveProfilePicture(int userId)
        {
            if (!TryAuthenticate(out _)) return Unauthorized();
            if (GetId() != userId) return Unauthorized();
            await m_userService.RemoveProfilePictureAsync(userId);
            return Ok();
        }

        #region private fields and constants
        private readonly IJwtService m_jwtService;
        private readonly IUserService m_userService;
        #endregion
    }
}