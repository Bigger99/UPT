using Microsoft.EntityFrameworkCore;
using UPT.Domain.Entities;

namespace UPT.Data;

public class UPTDbContext(DbContextOptions<UPTDbContext> options) : DbContext(options)
{
    public DbSet<User> Users => Set<User>();
    public DbSet<City> Cities => Set<City>();
    public DbSet<Trainer> Trainers => Set<Trainer>();
    public DbSet<Client> Clients => Set<Client>();
    public DbSet<Gym> Gyms => Set<Gym>();
    public DbSet<Chat> Chats => Set<Chat>();
    public DbSet<Feedback> Feedbacks => Set<Feedback>();
    public DbSet<Favorit> Favorits => Set<Favorit>();
    public DbSet<Payment> Payments => Set<Payment>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}