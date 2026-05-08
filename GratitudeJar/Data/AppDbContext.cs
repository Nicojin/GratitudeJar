using Microsoft.EntityFrameworkCore;
using GratitudeJar.Models;

namespace GratitudeJar.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<Entry> Entries => Set<Entry>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<User>().HasIndex(u => u.Username).IsUnique();

        builder.Entity<Entry>()
            .HasDiscriminator<string>("EntryType")
            .HasValue<Entry>("Entry")
            .HasValue<MemoryEntry>("Memory")
            .HasValue<MilestoneEntry>("Milestone")
            .HasValue<QuoteEntry>("Quote");

        builder.Entity<Entry>().HasQueryFilter(e => !e.IsDeleted);
    }
}
