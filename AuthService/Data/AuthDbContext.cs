using AuthService.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Data
{
    public class AuthDbContext : DbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .ToTable("User");

            modelBuilder.Entity<User>()
                .HasKey(x => x.IdUser);

            modelBuilder.Entity<User>()
                .Property(x => x.UserName)
                .HasMaxLength(255);

            modelBuilder.Entity<User>()
                .Property(x => x.Password)
                .HasMaxLength(255);

            modelBuilder.Entity<User>()
                .Property(x => x.Token)
                .HasMaxLength(255);
        }
    }
}