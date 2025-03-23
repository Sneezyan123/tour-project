using TourProject.Infrastructure.Funcs;
using TourProject.Models;
using TourProject.Models.DTOs;

namespace TourProject.Infrastructure.Extensions
{
    public static class UserExtensions
    {
        public static User Create(this UserDto userDto)
        {
            var user = new User()
            {
                Id = Guid.NewGuid(),
                Email = userDto.Email,
                Password = Hash.HashPassword(userDto.Password)
            };
            return user;
        }
        public static bool VerifyPassword(this User user, string hashedPassword)
        {
            return Hash.VerifyPassword(hashedPassword, user.Password);
        }
    }
}
