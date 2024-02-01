using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Praxisarbeit.Model;
using System;

public class AppDbContext
{
    private readonly IMongoDatabase _database;

    public AppDbContext(IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("MongoDbConnection");
        var mongoClient = new MongoClient(connectionString);
        _database = mongoClient.GetDatabase("Modul165");
    }

    public IMongoCollection<Order> Registrations => _database.GetCollection<Order>("Registrations");
    public IMongoCollection<Priority> Priorities => _database.GetCollection<Priority>("Priorities");
    public IMongoCollection<User> Users => _database.GetCollection<User>("Users");
    public IMongoCollection<Service> Services => _database.GetCollection<Service>("Services");

    public void SeedData()
    {
        // Users hinzufügen
        Users.InsertMany(new[]
        {
            new User() { Id = "1", userName = "admin", password = "admin" },
            new User() { Id = "2", userName = "Mitarbeiter1", password = "M1" },
            new User() { Id = "3", userName = "Mitarbeiter2", password = "M2" },
            new User() { Id = "4", userName = "Mitarbeiter3", password = "M3" },
            new User() { Id = "5", userName = "Mitarbeiter4", password = "M4" },
            new User() { Id = "6", userName = "Mitarbeiter5", password = "M5" },
            new User() { Id = "7", userName = "Mitarbeiter6", password = "M6" },
            new User() { Id = "8", userName = "Mitarbeiter7", password = "M7" },
            new User() { Id = "9", userName = "Mitarbeiter8", password = "M8" },
            new User() { Id = "10", userName = "Mitarbeiter9", password = "M9" },
            new User() { Id = "11", userName = "Mitarbeiter10", password = "M10" }
        }); 

        // Properties hinzufügen
        Priorities.InsertMany(new[]
        {
            new Priority() { Id = "1", PriorityType = "Tief", DaysToCompletion = 12 },
            new Priority() { Id = "2", PriorityType = "Standard", DaysToCompletion = 7 },
            new Priority() { Id = "3", PriorityType = "Express", DaysToCompletion = 5 }
        });

        // Service hinzufügen
        Services.InsertMany(new[]
        {
            new Service() { Id = "1", Beschreibung = "Kleiner Service" },
            new Service() { Id = "2", Beschreibung = "Grosser Service" },
            new Service() { Id = "3", Beschreibung = "Rennski-Service" },
            new Service() { Id = "4", Beschreibung = "Bindung montieren und einstellen" },
            new Service() { Id = "5", Beschreibung = "Fell zuschneiden" },
            new Service() { Id = "6", Beschreibung = "Heisswachsen" }
        });
    }
}
