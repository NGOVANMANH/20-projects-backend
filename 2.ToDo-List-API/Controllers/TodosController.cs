using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;

[ApiController]
[Route("api/[controller]")]
public class TodosController : ControllerBase
{
    private readonly AppDBContext _context;

    public TodosController(AppDBContext context)
    {
        _context = context;
    }
    [HttpGet]
    public async Task<IActionResult> GetTodos(int? page, int? pageSize)
    {
        var userId = HttpContext.Items["userId"]!.ToString();

        var todos = await _context.Todos
                                    .Where(t => t.UserId == ObjectId.Parse(userId))
                                    .Skip(((page ?? 1) - 1) * (pageSize ?? 10))
                                    .Take(pageSize ?? 10)
                                    .ToListAsync();

        return Ok(todos.Select(t => t.ToDto()));
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetTodo(string id)
    {
        var todo = await _context.Todos.FirstOrDefaultAsync(t => t.Id == ObjectId.Parse(id));
        return Ok(todo.ToDto());
    }
    [HttpPost]
    public async Task<IActionResult> CreateTodo(Todo todo)
    {
        await _context.Todos.AddAsync(new Todo
        {
            Content = todo.Content,
            Status = todo.Status,
            UserId = ObjectId.Parse(HttpContext.Items["userId"]!.ToString()),
        });
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetTodo), new { Id = todo.Id }, todo.ToDto());
    }
    [HttpPatch]
    public async Task<IActionResult> EditTodo(string id, Todo newTodo)
    {
        var todo = await _context.Todos.FirstOrDefaultAsync(t => t.Id == ObjectId.Parse(id));
        if (todo is null) return NotFound();
        todo.Content = newTodo.Content;
        todo.Status = newTodo.Status;
        await _context.SaveChangesAsync();
        return NoContent();
    }
    [HttpDelete]
    public async Task<IActionResult> DeleteTodo(string id)
    {
        _context.Todos.Remove(new Todo { Id = ObjectId.Parse(id) });
        await _context.SaveChangesAsync();
        return NoContent();
    }
}