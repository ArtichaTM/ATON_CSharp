using Microsoft.EntityFrameworkCore;

namespace AtonApi.Models;

public class UserContext(DbContextOptions<UserContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Configuring the Employee Entity to Represent Self-Referential Relationships
            modelBuilder.Entity<User>().HasOne(e => e.CreatedBy );
            modelBuilder.Entity<User>().HasOne(e => e.ModifiedBy);
            modelBuilder.Entity<User>().HasOne(e => e.RevokedBy );

            modelBuilder.Entity<User>().HasIndex(e => e.Login).IsUnique(true);
        }
}