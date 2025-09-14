using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StarEvents.DataAccess.Models;
using System.Reflection.Emit;

namespace StarEvents.DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<EventOrganizer> EventOrganizers { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Venue> Venues { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<BookingDetails> BookingDetails { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<ETicket> ETickets { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<LoyalityPointHistory> LoyaltyPointsHistories { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Booking>()
        .HasOne(b => b.Event)
        .WithMany(e => e.Bookings)
        .HasForeignKey(b => b.EventID)
        .OnDelete(DeleteBehavior.Restrict);

            // Booking -> Customer : cascade ok
            builder.Entity<Booking>()
                .HasOne(b => b.Customer)
                .WithMany(c => c.Bookings)
                .HasForeignKey(b => b.CustomerID)
                .OnDelete(DeleteBehavior.Cascade);

            // Payment 1:1 Booking : cascade ok
            builder.Entity<Payment>()
                .HasOne(p => p.Booking)
                .WithOne(b => b.Payment)
                .HasForeignKey<Payment>(p => p.BookingID)
                .OnDelete(DeleteBehavior.Cascade);

            // BookingDetails -> Booking : keep cascade
            builder.Entity<BookingDetails>()
                .HasOne(bd => bd.Booking)
                .WithMany(b => b.BookingDetails)
                .HasForeignKey(bd => bd.BookingID)
                .OnDelete(DeleteBehavior.Cascade);

            // BookingDetails -> Ticket : turn OFF cascade (critical fix)
            builder.Entity<BookingDetails>()
                .HasOne(bd => bd.Ticket)
                .WithMany(t => t.BookingDetails)
                .HasForeignKey(bd => bd.TicketID)
                .OnDelete(DeleteBehavior.NoAction);

            // Ticket -> Event : cascade (delete Event => delete Tickets)
            builder.Entity<Ticket>()
                .HasOne(t => t.Event)
                .WithMany(e => e.Tickets)
                .HasForeignKey(t => t.EventID)
                .OnDelete(DeleteBehavior.Cascade);

            // ETicket -> Booking : keep cascade
            builder.Entity<ETicket>()
                .HasOne(et => et.Booking)
                .WithMany(b => b.ETickets)
                .HasForeignKey(et => et.BookingID)
                .OnDelete(DeleteBehavior.Cascade);

            // ETicket -> Ticket : turn OFF cascade (mirror the BookingDetails fix)
            builder.Entity<ETicket>()
                .HasOne(et => et.Ticket)
                .WithMany(t => t.ETickets)
                .HasForeignKey(et => et.TicketID)
                .OnDelete(DeleteBehavior.NoAction);

            // Seed roles
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
