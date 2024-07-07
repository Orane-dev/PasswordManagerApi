using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Sqlite;
using PasswordManagerApi.Entities;
namespace PasswordManagerApi.Repository
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Password> Passwords { get; set; } = null!;
        public DbSet<TelegramUser> TelegramUsers { get; set; } = null!;

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { 
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Password>()
                .HasOne(p => p.TelegramUser)
                .WithMany()
                .HasForeignKey(p => p.TelegramUserId)
                .IsRequired();

            modelBuilder.Entity<TelegramUser>().HasData(
                new TelegramUser { TelegramId = "1994548862", Role = (int)ServiceRole.RoleType.Admin}
                );
        }
    }
}
