using MongoDB.Bson;

namespace _1.Personal_Blogging_Platform_API.Entities;

public class Blog
{
    public ObjectId Id { get; set; }
    public string Title { get; set; } = null!;
    public string Content { get; set; } = null!;
    public DateTime PublishingAt { get; set; }
    public IEnumerable<string>? Tags = null;
}