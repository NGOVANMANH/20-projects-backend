using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly AppDBContext _context;

    public UsersController(AppDBContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        var users = await _context.Users.ToListAsync();
        return Created("", users.Select(u => u.ToDto()));
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser(User user)
    {
        var newUser = await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        return Created("", newUser.Entity.ToDto());
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginUser(User user)
    {
        var loginUser = await _context.Users.FirstOrDefaultAsync(u => u.Username == user.Username && u.Password == user.Password);
        if (loginUser is null) return BadRequest();

        var cookieOpts = new CookieOptions
        {
            Expires = DateTime.UtcNow.AddMinutes(5),
        };

        HttpContext.Response.Cookies.Append("userId", loginUser.Id.ToString(), cookieOpts);

        return Ok();
    }
}