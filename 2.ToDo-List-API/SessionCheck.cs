public class SessionCheck
{
    private readonly RequestDelegate _next;
    public SessionCheck(RequestDelegate next)
    {
        _next = next;
    }
    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Path.StartsWithSegments("/api/users/login") ||
            context.Request.Path.StartsWithSegments("/api/users/register"))
        {
            await _next(context);
            return;
        }

        if (context.Request.Cookies.TryGetValue("userId", out var userId))
        {
            context.Items["userId"] = userId;
            await _next(context);
            return;
        }

        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
        await context.Response.WriteAsync("Unauthorized access. Please log in.");
        return;
    }
}