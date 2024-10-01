using MongoDB.Bson;

public class User
{
    public ObjectId Id { get; set; }
    public string Username { get; set; } = null!;
    public string HashedPassword { get; set; } = null!;
}