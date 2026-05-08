using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using GratitudeJar.Data;
using GratitudeJar.Models;

namespace GratitudeJar.Services;

public class AuthService
{
    private readonly AppDbContext _db;
    private readonly IHttpContextAccessor _http;

    public AuthService(AppDbContext db, IHttpContextAccessor http)
    {
        _db = db;
        _http = http;
    }

    public async Task<User?> GetCurrentUserAsync()
    {
        var ctx = _http.HttpContext;
        if (ctx is null) return null;

        var idStr = ctx.User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (!int.TryParse(idStr, out var id)) return null;

        return await _db.Users.FirstOrDefaultAsync(u => u.Id == id);
    }
}
