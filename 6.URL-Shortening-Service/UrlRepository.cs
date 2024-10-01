using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;

public class UrlRepository
{
    private readonly UrlShortenerDb _context;
    public UrlRepository(UrlShortenerDb context)
    {
        _context = context;
    }
    public async Task<string?> GetUrlAsync(string id)
    {
        var url = await _context.Urls.FirstOrDefaultAsync(u => u.Id == ObjectId.Parse(id));
        return url?.LongUrl;
    }
    public async Task<string> CreateShortUrlAsync(string url)
    {
        var newUrl = new Url
        {
            LongUrl = url,
        };
        var createdUrl = await _context.Urls.AddAsync(newUrl);
        await _context.SaveChangesAsync();

        return $"http://localhost:5143/{createdUrl.Entity.Id.ToString()}";
    }
}