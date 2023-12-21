using Library.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Library.Api.Identity
{
    public class ApplicationIdentityDbContext : IdentityDbContext<User>
    {
        public ApplicationIdentityDbContext(DbContextOptions<ApplicationIdentityDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Define Roles

            var adminRoleId = Guid.NewGuid().ToString();
            var visitorRoleId = Guid.NewGuid().ToString();

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = adminRoleId,
                    ConcurrencyStamp = adminRoleId,
                    Name = "Admin",
                    NormalizedName = "Admin".ToUpper()
                },
                new IdentityRole
                {
                    Id = visitorRoleId,
                    ConcurrencyStamp = visitorRoleId,
                    Name = "Visitor",
                    NormalizedName = "Visitor".ToUpper()
                },
            };

            // Define Admin

            var adminId = Guid.NewGuid().ToString();
            var admin = new User
            {
                Id = adminId,
                FullName = "Admin",
                Email = "admin@example.com",
                NormalizedEmail = "admin@example.com".ToUpper(),
                UserName = "admin",
                NormalizedUserName = "admin".ToUpper(),
                UpdatedAt = DateTime.Now,
                CreatedAt = DateTime.Now,
            };
            PasswordHasher<User> ph = new PasswordHasher<User>();
            admin.PasswordHash = ph.HashPassword(admin, "1234");

            // Define Admin Role 

            var adminRole = new IdentityUserRole<string>
            {
                RoleId = adminRoleId,
                UserId = adminId,
            };

            // Seed Data

            builder.Entity<IdentityRole>().HasData(roles);
            builder.Entity<User>().HasData(admin);
            builder.Entity<IdentityUserRole<string>>().HasData(adminRole);
        }
    }
}
