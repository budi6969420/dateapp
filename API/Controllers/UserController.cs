// ------------------------------------------------------------------
// © Copyright 2024 Thermo Fisher Scientific Inc. All rights reserved.
// ------------------------------------------------------------------
using DateAppApi.Core;
using DateAppApi.DbContexts;
using DateAppApi.Dtos;
using DateAppApi.Dtos.User;
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

        [HttpGet]
        public async Task<IActionResult> Get(int id)
        {
            if (!TryAuthenticate(out _)) return Unauthorized();
            var user = await m_userService.GetUserAsync(id);
            if (user == null) return NotFound();
            return Ok(user.ToDto());
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if (!TryAuthenticate(out _)) return Unauthorized();
            var users = await m_userService.GetAllUserAsync();
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
            var user = await m_userService.RegisterAsync(registerDto.Username, registerDto.Password);
            var token = m_jwtService.GenerateJwtToken(user);
            return Ok(new TokenDto()
            {
                Token = token
            });
        }

        #region private fields and constants
        private readonly IJwtService m_jwtService;
        private readonly IUserService m_userService;
        #endregion
    }
}