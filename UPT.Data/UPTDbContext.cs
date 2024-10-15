using Microsoft.EntityFrameworkCore;
using UPT.Domain.Entities;

namespace UPT.Data;

public class UPTDbContext(DbContextOptions<UPTDbContext> options) : DbContext(options)
{
    public DbSet<Person> Areas => Set<Person>();
    public DbSet<City> Users => Set<City>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}