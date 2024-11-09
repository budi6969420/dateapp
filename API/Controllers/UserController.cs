// ------------------------------------------------------------------
// © Copyright 2024 Thermo Fisher Scientific Inc. All rights reserved.
// ------------------------------------------------------------------
using DateAppApi.Dtos;
using DateAppApi.IServices;
using Microsoft.AspNetCore.Mvc;

namespace DateAppApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UserController : ControllerBase
    {
        public UserController(IJwtService jwtService,
            IUserService userService)
        {
            m_jwtService = jwtService;
            m_userService = userService;
        }

        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var user = await m_userService.EnsureLoginOkAsync(loginDto.Username, loginDto.Password);
            var token = m_jwtService.GenerateJwtToken(user);
            return Ok(new TokenDto()
            {
                Token = token
            });
        }

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