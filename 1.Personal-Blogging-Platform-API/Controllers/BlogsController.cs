using _1.Personal_Blogging_Platform_API.Data;
using _1.Personal_Blogging_Platform_API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;

namespace _1.Personal_Blogging_Platform_API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BlogsController : ControllerBase
{
    private readonly MongoDbContext _context;

    public BlogsController(MongoDbContext context)
    {
        _context = context;
    }
    [HttpGet]
    public async Task<IActionResult> GetBlogs(int? page, int? pageSize)
    {
        int currentPage = page ?? 1;
        int currentPageSize = pageSize ?? 10;
        if (currentPage < 1 || currentPageSize < 1)
        {
            return BadRequest("Page and page size must be greater than 0.");
        }

        var blogs = await _context.Blogs
                                    .Skip((currentPage - 1) * currentPageSize)
                                    .Take(currentPageSize)
                                    .ToListAsync();

        return Ok(new { data = blogs.Select(b => b.ToDto()) });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetBlog(string id)
    {
        var blog = await _context.Blogs
                                    .FirstOrDefaultAsync(blog => blog.Id == ObjectId.Parse(id));

        return Ok(blog.ToDto());
    }
    [HttpPost]
    public async Task<IActionResult> CreateBlog(Blog blog)
    {
        await _context.Blogs.AddAsync(blog);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetBlog), new { Id = blog.Id }, blog.ToDto());
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBlog(string id)
    {
        _context.Blogs.Remove(new Blog { Id = ObjectId.Parse(id) });
        await _context.SaveChangesAsync();
        return NoContent();
    }
    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdateBlog(string id, Blog newBlog)
    {
        var existingBlog = await _context.Blogs.FindAsync(ObjectId.Parse(id));
        if (existingBlog == null)
        {
            return NotFound();
        }

        existingBlog.Title = newBlog.Title;
        existingBlog.Content = newBlog.Content;

        await _context.SaveChangesAsync();

        return NoContent();
    }

}
