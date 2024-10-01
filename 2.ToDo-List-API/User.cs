using MongoDB.Bson;

public class User
{
    public ObjectId Id { get; set; }
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
}

public static class UserMapper
{
    public static Object? ToDto(this User? user)
    {
        return user is null ? null : new
        {
            Id = user.Id.ToString(),
            Content = user.Username,
            Status = user.Password,
        };
    }
}