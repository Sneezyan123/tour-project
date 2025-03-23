using AutoMapper.Configuration.Annotations;
using Microsoft.EntityFrameworkCore;
using TourProject.Infrastructure.Extensions;
using TourProject.Infrastructure.Funcs;
using TourProject.Models;
using TourProject.Models.DTOs;
using TourProject.Persistence;

namespace TourProject.Services
{
    public class UserService
    {
        private readonly ApiDbContext _context;
        private readonly JwtProvider _jwtProvider;
        public UserService(ApiDbContext context, JwtProvider jwtProvider)
        {
            _jwtProvider = jwtProvider;
            _context = context;
        }
        public async Task<string> Register(UserDto userDto)
        {
            var isUser = await _context.Users.AnyAsync(u => u.Email == userDto.Email);
            if (isUser)
            {
                throw new Exception("there is such User!");
            }
            var user = userDto.Create();
            var StringRoles = userDto.Roles.Select(r => r.ToString());
            var UserRoles = _context.Roles.Where(r => StringRoles.Contains(r.Name))
                .Select(r=> new UserRole() {Role=r, User = user }).ToList();

            user.UserRoles.AddRange(UserRoles);
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            var token = _jwtProvider.CreateToken(user);
            return token;
        }
        public async Task<string> Login(LoginPasswordUserDto userDto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == userDto.Email);
            if (user is null || !user.VerifyPassword(userDto.Password))
            {
                throw new Exception("login or password is not correct!");
            }
            var token = _jwtProvider.CreateToken(user);
            return token;
        }
    }
}
