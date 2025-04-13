using ApplicationCore.Domain.Models;
using ApplicationCore.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EF;

public class AppDbContext: DbContext
{
    public DbSet<Movie> Movies { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<User> Users { get; set; }

    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite(@"data source=c:\data\appdb.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Movie>(b =>
        {
            b.HasMany(m => m.Reviews)
                .WithOne()
                .HasForeignKey(r => r.MovieId);
            b.HasKey(m => m.Id);
            b.Property(m => m.CreatedAt)
                .HasDefaultValue(DateTime.Now);
        });

        modelBuilder.Entity<Review>(b =>
        {
            b.HasKey(r => r.Id);
            b.Property(r => r.Rate)
                .HasConversion(
                    r => r.Value,
                    r => Rate.Of(r)
                );
        });

        modelBuilder.Entity<User>(b =>
        {
            b.HasKey(u => u.Id);
        });
    }
}