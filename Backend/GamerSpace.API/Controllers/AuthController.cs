using GamerSpace.Application.DTOs;
using GamerSpace.Application.UseCases.Users.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GamerSpace.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IRegisterUserCommand _registerUserCommand;
        private readonly ILoginCommand _loginCommand;

        public AuthController(IRegisterUserCommand registerUserCommand, ILoginCommand loginCommand)
        {
            _registerUserCommand = registerUserCommand;
            _loginCommand = loginCommand;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterUserDto registerUserDto)
        {
            var userDto = await _registerUserCommand.Execute(registerUserDto);
            return CreatedAtAction(nameof(Register), new { id = userDto.Id }, userDto);
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginDto LoginDto)
        {
            var responseDto = await _loginCommand.Execute(LoginDto);
            return Ok(responseDto);
        }
    }
}