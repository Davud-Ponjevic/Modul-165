namespace Praxisarbeit.Model;

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class Order
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonRepresentation(BsonType.ObjectId)]
    public string UserId { get; set; }

    [BsonRepresentation(BsonType.ObjectId)]
    public string PriorityId { get; set; }

    [BsonRepresentation(BsonType.ObjectId)]
    public string ServiceId { get; set; }

    public string Name { get; set; }

    [EmailAddress]
    public string Email { get; set; }

    [Phone]
    public string Phone { get; set; }

    public DateTime CreateDate { get; set; }

    public DateTime PickupDate { get; set; }
}



