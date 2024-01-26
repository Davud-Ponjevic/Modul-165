namespace Praxisarbeit.Model;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class Priority
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    public string PriorityType { get; set; }

    public int DaysToCompletion { get; set; }
}


