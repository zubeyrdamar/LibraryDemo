using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Library.Api.Identity
{
    public class ApplicationIdentityDbContext : IdentityDbContext<IdentityUser>
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
                    Name = CustomUserRoles.Admin.ToString(),
                    NormalizedName = CustomUserRoles.Admin.ToString().ToUpper()
                },
                new IdentityRole
                {
                    Id = visitorRoleId,
                    ConcurrencyStamp = visitorRoleId,
                    Name = CustomUserRoles.Visitor.ToString(),
                    NormalizedName = CustomUserRoles.Visitor.ToString().ToUpper()
                },
            };

            // Define Admin

            var adminId = Guid.NewGuid().ToString();
            var admin = new IdentityUser
            {
                Id = adminId,
                Email = "admin@example.com",
                NormalizedEmail = "admin@example.com".ToUpper(),
                UserName = "admin",
                NormalizedUserName = "admin".ToUpper()
            };
            PasswordHasher<IdentityUser> ph = new PasswordHasher<IdentityUser>();
            admin.PasswordHash = ph.HashPassword(admin, "1234");

            // Define Admin Role 

            var adminRole = new IdentityUserRole<string>
            {
                RoleId = adminRoleId,
                UserId = adminId,
            };

            // Seed Data

            builder.Entity<IdentityRole>().HasData(roles);
            builder.Entity<IdentityUser>().HasData(admin);
            builder.Entity<IdentityUserRole<string>>().HasData(adminRole);
        }
    }

    public enum CustomUserRoles
    {
        Admin,
        Visitor
    }
}
