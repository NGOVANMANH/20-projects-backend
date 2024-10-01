using MongoDB.Bson;

public class TaskData
{
    public ObjectId Id { get; set; }
    public string Content { get; set; } = null!;
}