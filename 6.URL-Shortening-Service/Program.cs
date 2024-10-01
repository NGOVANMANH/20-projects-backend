using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using ZstdSharp.Unsafe;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<UrlShortenerDb>(opt =>
opt.UseMongoDB(new MongoClient("mongodb://localhost:27017"), "ShortenerUrlDb"));

builder.Services.AddScoped<UrlShortenerService>();
builder.Services.AddScoped<UrlRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("api/shorted-link/", async (Url url, UrlShortenerService urlShortenerService) =>
{
    var longUrl = url.LongUrl;

    if (string.IsNullOrEmpty(longUrl))
    {
        return Results.BadRequest("Url is required");
    }

    var shortUrl = await urlShortenerService.ShorteningAsync(longUrl);

    return Results.Ok(shortUrl);
});

app.MapGet("{link}", async (string link, UrlShortenerService urlShortenerService) =>
{
    var url = await urlShortenerService.GetUrlAsync(link);

    if (url == null)
    {
        return Results.NotFound();
    }

    return Results.Redirect(url);
});

app.Run();