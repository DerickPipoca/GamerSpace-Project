
using GamerSpace.Application.DTOs;
using GamerSpace.Application.UseCases.Users.Commands;
using GamerSpace.Domain.Entities;
using GamerSpace.Domain.Interfaces;
using GamerSpace.Infrastructure.Persistence;
using GamerSpace.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GamerSpace.API.Seed
{
    public class SeedData
    {
        private readonly GamerSpaceDbContext _context;
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        private readonly ICreateAdminUserCommand _createAdminUserCommand;

        public SeedData(GamerSpaceDbContext context, IUserRepository userRepository, IConfiguration configuration, ICreateAdminUserCommand createAdminUserCommand)
        {
            _context = context;
            _userRepository = userRepository;
            _configuration = configuration;
            _createAdminUserCommand = createAdminUserCommand;
        }

        public async Task SeedAsync()
        {

            if (!await _context.Users.AnyAsync())
            {
                var adminEmail = "admin@gamerspace.com";
                var adminPassword = _configuration["SeedConfig:AdminPassword"];

                if (string.IsNullOrEmpty(adminPassword))
                {
                    throw new InvalidOperationException("A senha do administrador não foi configurada.");
                }

                var user = await _userRepository.GetUserByEmailAsync(adminEmail);
                if (user == null)
                {
                    RegisterUserDto registerUser = new() { FullName = "Admin", Email = adminEmail, Password = adminPassword };
                    await _createAdminUserCommand.Execute(registerUser);
                }
            }

            if (!await _context.ClassificationTypes.AnyAsync())
            {
                await _context.ClassificationTypes.AddRangeAsync([
                    new ClassificationType("Cores"), // Id: 1
                    new ClassificationType("Tags"), // Id: 2
                    new ClassificationType("Departamento") // Id: 3
                    ]
                );
                await _context.SaveChangesAsync();
            }

            if (!await _context.Categories.AnyAsync())
            {
                await _context.Categories.AddRangeAsync([
                    new Category("Preto",1),            // Id: 1
                    new Category("Branco",1),           // Id: 2
                    new Category("Bege",1),             // Id: 3
                    new Category("Vermelho",1),         // Id: 4
                    new Category("Wireless",2),         // Id: 5
                    new Category("Audio 7.1",2),        // Id: 6
                    new Category("Mouse",3),            // Id: 7
                    new Category("Teclado",3),          // Id: 8
                    new Category("Mouse pad",3),        // Id: 9
                    new Category("Headset",3),          // Id: 10
                    new Category("Kit",3),              // Id: 11
                    new Category("Caixa de som",3),     // Id: 12
                    new Category("Microfone",3),        // Id: 13
                    new Category("Webcam",3)            // Id: 14
                    ]
                );
                await _context.SaveChangesAsync();
            }
        }
    }
}