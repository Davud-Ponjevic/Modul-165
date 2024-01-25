using Microsoft.EntityFrameworkCore;
using Praxisarbeit.Model;
using System.Security.Cryptography;
using MongoDB.Driver;

public class AppDbContext : DbContext
{
    private readonly IMongoDatabase _database;

    public AppDbContext(DbContextOptions<AppDbContext> options, IMongoDatabase database) : base(options)
    {
        _database = database;
    }

    public IMongoCollection<Order> Registrations => _database.GetCollection<Order>("Registrations");
    public IMongoCollection<Priority> Priorities => _database.GetCollection<Priority>("Priorities");
    public IMongoCollection<User> Users => _database.GetCollection<User>("Users");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Users hinzufügen
        modelBuilder.Entity<User>().HasData(
            new User() { Id = 1, UserName = "admin", Password = "admin" },
            new User() { Id = 2, UserName = "Mitarbeiter1", Password = "M1" },
            new User() { Id = 3, UserName = "Mitarbeiter2", Password = "M2" },
            new User() { Id = 4, UserName = "Mitarbeiter3", Password = "M3" },
            new User() { Id = 5, UserName = "Mitarbeiter4", Password = "M4" },
            new User() { Id = 6, UserName = "Mitarbeiter5", Password = "M5" },
            new User() { Id = 7, UserName = "Mitarbeiter6", Password = "M6" },
            new User() { Id = 8, UserName = "Mitarbeiter7", Password = "M7" },
            new User() { Id = 9, UserName = "Mitarbeiter8", Password = "M8" },
            new User() { Id = 10, UserName = "Mitarbeiter9", Password = "M9" },
            new User() { Id = 11, UserName = "Mitarbeiter10", Password = "M10" }
        );

        // Properties hinzufügen
        modelBuilder.Entity<Priority>().HasData(
            new Priority() { Id = 1, PriorityType = "Tief", DaysToCompletion = 12 },
            new Priority() { Id = 2, PriorityType = "Standard", DaysToCompletion = 7 },
            new Priority() { Id = 3, PriorityType = "Express", DaysToCompletion = 5 }
        );

        // Service hinzufügen
        modelBuilder.Entity<Service>().HasData(
            new Service() { Id = 1, Beschreibung = "Kleiner Service" },
            new Service() { Id = 2, Beschreibung = "Grosser Service" },
            new Service() { Id = 3, Beschreibung = "Rennski-Service" },
            new Service() { Id = 4, Beschreibung = "Bindung montieren und einstellen" },
            new Service() { Id = 5, Beschreibung = "Fell zuschneiden" },
            new Service() { Id = 6, Beschreibung = "Heisswachsen" }
        );
    }
}
