
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
        private readonly ICreateAdminUserCommand _createAdminUserCommand;

        public SeedData(GamerSpaceDbContext context, IUserRepository userRepository, ICreateAdminUserCommand createAdminUserCommand)
        {
            _context = context;
            _userRepository = userRepository;
            _createAdminUserCommand = createAdminUserCommand;
        }

        public async Task SeedAsync()
        {

            if (!_context.Users.Any())
            {
                const string adminEmail = "admin@gamerspace.com";
                const string adminPassword = "Admin@123456";

                var user = await _userRepository.GetUserByEmailAsync(adminEmail);
                if (user == null)
                {
                    RegisterUserDto registerUser = new() { FullName = "Admin", Email = adminEmail, Password = adminPassword };
                    await _createAdminUserCommand.Execute(registerUser);
                }
            }

            if (!_context.ClassificationTypes.Any())
            {
                _context.ClassificationTypes.AddRange([
                    new ClassificationType("Cores"), // Id: 1
                    new ClassificationType("Tags"), // Id: 2
                    new ClassificationType("Departamento") // Id: 3
                    ]
                );
                await _context.SaveChangesAsync();
            }

            if (!_context.Categories.Any())
            {
                _context.Categories.AddRange([
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