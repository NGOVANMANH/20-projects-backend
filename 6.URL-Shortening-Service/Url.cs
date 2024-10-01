using MongoDB.Bson;

public class Url
{
    public ObjectId Id { get; set; }
    public string LongUrl { get; set; } = null!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}