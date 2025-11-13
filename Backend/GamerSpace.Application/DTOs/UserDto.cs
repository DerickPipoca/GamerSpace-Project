using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GamerSpace.Application.DTOs
{
    public class UserDto
    {
        public long Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
    }
}