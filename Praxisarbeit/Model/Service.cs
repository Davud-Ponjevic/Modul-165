namespace Praxisarbeit.Model;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class Service
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    public string Beschreibung { get; set; }
}

