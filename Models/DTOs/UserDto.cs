using System.ComponentModel.DataAnnotations;
using TourProject.Infrastructure.Enums;

namespace TourProject.Models.DTOs
{
    public class UserDto
    {
        [EmailAddress]
        public string Email { get; set; }
        [MinLength(6)]
        public string Password { get; set; }
        public List<RoleEnum> Roles { get; set; } = [];
    }
}
