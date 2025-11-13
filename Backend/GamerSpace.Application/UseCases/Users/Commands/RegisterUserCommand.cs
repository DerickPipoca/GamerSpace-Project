using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GamerSpace.Application.DTOs;
using GamerSpace.Application.Services.Auth;
using GamerSpace.Domain.Entities;
using GamerSpace.Domain.Interfaces;

namespace GamerSpace.Application.UseCases.Users.Commands
{
    public interface IRegisterUserCommand
    {
        Task<UserDto> Execute(RegisterUserDto registerUserDto);
    }
    public class RegisterUserCommand : IRegisterUserCommand
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IPasswordService _passwordService;


        public RegisterUserCommand(IUserRepository userRepository, IUnitOfWork unitOfWork, IMapper mapper, IPasswordService passwordService)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _passwordService = passwordService;
        }

        public async Task<UserDto> Execute(RegisterUserDto registerUserDto)
        {
            var existingUser = await _userRepository.GetUserByEmailAsync(registerUserDto.Email);

            if (existingUser != null)
                throw new InvalidOperationException("This email is already registered.");

            var passwordHash = _passwordService.HashPassword(registerUserDto.Password);

            var user = new User(registerUserDto.FullName, registerUserDto.Email, passwordHash, "Customer");

            await _userRepository.AddAsync(user);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<UserDto>(user);
        }
    }
}