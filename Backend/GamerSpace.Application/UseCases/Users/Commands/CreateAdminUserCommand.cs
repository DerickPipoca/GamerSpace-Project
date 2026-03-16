using AutoMapper;
using GamerSpace.Application.DTOs;
using GamerSpace.Application.Services.Auth;
using GamerSpace.Domain.Entities;
using GamerSpace.Domain.Interfaces;

namespace GamerSpace.Application.UseCases.Users.Commands
{
    public interface ICreateAdminUserCommand
    {
        Task Execute(RegisterUserDto registerUserDto);
    }
    public class CreateAdminUserCommand : ICreateAdminUserCommand
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordService _passwordService;


        public CreateAdminUserCommand(IUserRepository userRepository, IUnitOfWork unitOfWork, IPasswordService passwordService)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _passwordService = passwordService;
        }

        public async Task Execute(RegisterUserDto registerUserDto)
        {
            var passwordHash = _passwordService.HashPassword(registerUserDto.Password);

            var user = new User(registerUserDto.FullName, registerUserDto.Email, passwordHash, "Admin");

            await _userRepository.AddAsync(user);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}