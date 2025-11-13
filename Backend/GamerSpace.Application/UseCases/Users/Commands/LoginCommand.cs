using GamerSpace.Application.DTOs;
using GamerSpace.Application.Services.Auth;
using GamerSpace.Domain.Interfaces;

namespace GamerSpace.Application.UseCases.Users.Commands
{
    public interface ILoginCommand
    {
        Task<LoginResponseDto> Execute(LoginDto loginDto);
    }

    public class LoginCommand : ILoginCommand
    {
        private readonly IPasswordService _passwordService;
        private readonly IJwtTokenGeneratorService _jwtTokenGeneratorService;
        private readonly IUserRepository _userRepository;

        public LoginCommand(IPasswordService passwordService, IJwtTokenGeneratorService jwtTokenGeneratorService, IUserRepository userRepository)
        {
            _passwordService = passwordService;
            _jwtTokenGeneratorService = jwtTokenGeneratorService;
            _userRepository = userRepository;
        }

        public async Task<LoginResponseDto> Execute(LoginDto loginDto)
        {
            var login = await _userRepository.GetUserByEmailAsync(loginDto.Email);

            if (login == null)
                throw new InvalidOperationException("Invalid email or password.");

            var isPasswordValid = _passwordService.VerifyPassword(loginDto.Password, login.PasswordHash);

            if (!isPasswordValid)
                 throw new InvalidOperationException("Invalid email or password.");

            var token = _jwtTokenGeneratorService.GenerateToken(login);

            return new LoginResponseDto(token);
        }
    }
}