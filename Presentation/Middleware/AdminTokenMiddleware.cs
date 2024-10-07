using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Presentation.Middleware;

public class AdminTokenMiddleware
{
    private readonly RequestDelegate _next;
    private readonly string _adminToken;

    public AdminTokenMiddleware(RequestDelegate next, IConfiguration config)
    {
        _next = next;
        _adminToken = config["AdminSettings:AdminToken"];
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Only check token if the route starts with /admin
        if (context.Request.Path.StartsWithSegments("/admin"))
        {
            var token = context.Request.Headers["Authorization"].ToString();

            if (string.IsNullOrEmpty(token) || token != _adminToken)
            {
                context.Response.StatusCode = 403; // Forbidden
                await context.Response.WriteAsync("Unauthorized: Invalid Admin Token");
                return;
            }
        }

        // Proceed to the next middleware if not an admin route or if token is valid
        await _next(context);
    }
}