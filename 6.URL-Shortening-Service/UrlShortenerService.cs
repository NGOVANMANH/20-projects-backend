public class UrlShortenerService
{
    private readonly UrlRepository _urlRepository;

    public UrlShortenerService(UrlRepository urlRepository)
    {
        _urlRepository = urlRepository;
    }
    public async Task<string?> GetUrlAsync(string link)
    {
        var url = await _urlRepository.GetUrlAsync(link);
        return url;
    }
    public async Task<string?> ShorteningAsync(string link)
    {
        var shortUrl = await _urlRepository.CreateShortUrlAsync(link);
        return shortUrl;
    }
}