namespace _1.Personal_Blogging_Platform_API.Entities;

public class BlogRead
{
    public string Id { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string Content { get; set; } = null!;
    public DateTime PublishingAt { get; set; }
    public IEnumerable<string>? Tags = null;
}

public static class BlogMapper
{
    public static BlogRead? ToDto(this Blog? blog)
    {
        if (blog is null) return null;
        return new BlogRead
        {
            Id = blog.Id.ToString(),
            Title = blog.Title,
            Content = blog.Content,
            PublishingAt = blog.PublishingAt,
            Tags = blog.Tags,
        };
    }
}