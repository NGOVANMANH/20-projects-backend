using MongoDB.Bson;

public class Todo
{
    public ObjectId Id { get; set; }
    public string Content { get; set; } = null!;
    public bool Status { get; set; }
    public ObjectId UserId { get; set; }
    public User? User { get; set; }
}

public static class TodoMapper
{
    public static Object? ToDto(this Todo? todo)
    {
        return todo is null ? null : new
        {
            Id = todo.Id.ToString(),
            Content = todo.Content,
            Status = todo.Status,
            UserId = todo.UserId.ToString(),
            User = todo.User.ToDto(),
        };
    }
}