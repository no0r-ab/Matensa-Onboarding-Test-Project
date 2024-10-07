using Microsoft.EntityFrameworkCore;
using Domain.Users;
using Domain.Transactions;

namespace Infrastructure
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserToken> UserTokens { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // UserToken configuration without foreign key relation
            modelBuilder.Entity<UserToken>(entity =>
            {
                entity.HasIndex(t => t.Token).IsUnique();
                entity.Property(t => t.Token).IsRequired();
                entity.Property(t => t.CreationDate).IsRequired();
                entity.Property(t => t.ExpireDate).IsRequired();
            });

            // User entity configuration
            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(u => u.Email).IsRequired();
                entity.HasIndex(u => u.Email).IsUnique();

                entity.HasMany(u => u.SentTransactions)
                      .WithOne()
                      .HasForeignKey(t => t.SenderId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(u => u.ReceivedTransactions)
                      .WithOne()
                      .HasForeignKey(t => t.ReceiverId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Transaction entity configuration (if needed)
            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.Property(t => t.Amount).IsRequired();

            });
        }
    }
}
