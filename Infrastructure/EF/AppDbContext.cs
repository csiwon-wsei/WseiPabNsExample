using ApplicationCore.Domain.Models;
using ApplicationCore.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EF;

public class AppDbContext : DbContext
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
        optionsBuilder.UseSqlite("data source=c:\\data\\app.db");
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Movie>(b =>
        {
            b.HasKey(m => m.Id);
            b.HasMany(m => m.Reviews)
                .WithOne()
                .HasForeignKey(m => m.MovieId);
        });
        
        builder.Entity<Review>(b =>
        {
            b.HasKey(r => r.Id);
            b.Property(m => m.Rate)
                .HasConversion(r => r.Value, r => Rate.Of(r));
        });
    }
}