using Microsoft.EntityFrameworkCore;
using TourProject.Infrastructure.Enums;
using TourProject.Models;

namespace TourProject.Persistence
{
    public class ApiDbContext: DbContext
    {
        public ApiDbContext(DbContextOptions options): base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserRole>()
                .HasKey(ur => new { ur.UserId, ur.RoleId });
            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId);

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.Role)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.RoleId);


            var Admin = new Role()
            {
                Id = 1,
                Name = RoleEnum.admin.ToString()
            };
            var User = new Role()
            {
                Id = 2,
                Name = RoleEnum.user.ToString()
            };

            modelBuilder.Entity<Role>()
                .HasData(Admin, User);
        }

    }
}
