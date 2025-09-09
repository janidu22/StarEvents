using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StarEvents.DataAccess.Models;

namespace StarEvents.DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<IdentityRole>().HasData(

                new IdentityRole
                {
                    Id = "bf0c2771-2974-40ef-8309-0ceb0d2bc93b",
                    Name = "User",
                    NormalizedName = "USER",
                },

                new IdentityRole
                {
                    Id = "41828726-e6de-49c8-a669-2f77fb515474",
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                },

                new IdentityRole
                {
                    Id = "2be05559-cf59-4ae1-9ec0-2f2a8447d2d1",
                    Name = "Event Organizer",
                    NormalizedName = "EVENT ORGANIZER",
                }
            );

            var hasher = new PasswordHasher<ApplicationUser>();

            builder.Entity<ApplicationUser>().HasData(

                new ApplicationUser
                {
                    Id = "c3643f15-0620-4af9-a729-4fcd4a4fb8bd",
                    UserName = "Janidu",
                    NormalizedUserName = "JANIDU",
                    Email = "janidu@test.com",
                    NormalizedEmail = "JANIDU@TEST.COM",
                    PasswordHash = hasher.HashPassword(null, "janidu22$#Q"),
                    EmailConfirmed = true,
                    FullName = "Janidu Dhakshitha Yapa",
                    LoyaltyPoints = 1000
                },
                 new ApplicationUser
                 {
                     Id = "e2599235-a01c-4a69-851b-5e569d4b1b35",
                     UserName = "Admin",
                     NormalizedUserName = "ADMIN",
                     Email = "admin@test.com",
                     NormalizedEmail = "ADMIN@TEST.COM",
                     PasswordHash = hasher.HashPassword(null, "janidu22$#Q"),
                     EmailConfirmed = true,
                     FullName = "Star Events Admin",
                     LoyaltyPoints = 1000
                 },
                  new ApplicationUser
                  {
                      Id = "986134cf-47f7-4c76-bdaa-faec8504723f",
                      UserName = "Orgarnizer",
                      NormalizedUserName = "ORGARNIZER",
                      Email = "orgarnizer@test.com",
                      NormalizedEmail = "ORGARNIZER@TEST.COM",
                      PasswordHash = hasher.HashPassword(null, "janidu22$#Q"),
                      EmailConfirmed = true,
                      FullName = "Star Events Organizer",
                      LoyaltyPoints = 1000
                  }
            );

            builder.Entity<IdentityUserRole<string>>().HasData(

            new IdentityUserRole<string>
            {
                RoleId = "bf0c2771-2974-40ef-8309-0ceb0d2bc93b",
                UserId = "c3643f15-0620-4af9-a729-4fcd4a4fb8bd"
            },
            
            new IdentityUserRole<string>
            {
                RoleId = "41828726-e6de-49c8-a669-2f77fb515474",
                UserId = "e2599235-a01c-4a69-851b-5e569d4b1b35"
            }, 
            
            new IdentityUserRole<string>
            {
                RoleId = "2be05559-cf59-4ae1-9ec0-2f2a8447d2d1",
                UserId = "986134cf-47f7-4c76-bdaa-faec8504723f"
            }


            );
        }
    }
}
