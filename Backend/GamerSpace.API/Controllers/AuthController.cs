using GamerSpace.Application.DTOs;
using GamerSpace.Application.UseCases.Users.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GamerSpace.API.Controllers
{
    /// <summary>
    /// Gerencia a autenticação e o registro de novos usuários na plataforma.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class AuthController : ControllerBase
    {
        private readonly IRegisterUserCommand _registerUserCommand;
        private readonly ILoginCommand _loginCommand;

        public AuthController(IRegisterUserCommand registerUserCommand, ILoginCommand loginCommand)
        {
            _registerUserCommand = registerUserCommand;
            _loginCommand = loginCommand;
        }

        /// <summary>
        /// Registra um novo usuário no sistema.
        /// </summary>
        /// <param name="registerUserDto">Dados necessários para criar a conta do usuário (ex: nome, e-mail, senha).</param>
        /// <returns>Os detalhes do usuário recém-criado.</returns>
        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterUserDto registerUserDto)
        {
            var userDto = await _registerUserCommand.Execute(registerUserDto);
            return CreatedAtAction(nameof(Register), new { id = userDto.Id }, userDto);
        }

        /// <summary>
        /// Realiza o login de um usuário e gera o token de autenticação (JWT).
        /// </summary>
        /// <param name="loginDto">Credenciais de acesso do usuário (e-mail e senha).</param>
        /// <returns>O token JWT e as informações da sessão do usuário logado.</returns>
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var responseDto = await _loginCommand.Execute(loginDto);
            return Ok(responseDto);
        }
    }
}