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

   
}
