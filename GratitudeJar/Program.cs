using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using GratitudeJar.Components;
using GratitudeJar.Data;
using GratitudeJar.Interfaces;
using GratitudeJar.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(opt =>
    {
        opt.LoginPath = "/login";
        opt.LogoutPath = "/account/logout";
        opt.ExpireTimeSpan = TimeSpan.FromDays(30);
        opt.SlidingExpiration = true;
    });

builder.Services.AddAuthorization();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<IEntryRepository, EntryDatabase>();

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

if (!app.Environment.IsDevelopment())
    app.UseHsts();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseAntiforgery();
app.MapStaticAssets();

app.MapPost("/account/login", async (HttpContext ctx, AppDbContext db) =>
{
    var form = await ctx.Request.ReadFormAsync();
    var username = form["username"].ToString().Trim();
    var password = form["password"].ToString();

    var user = await db.Users.FirstOrDefaultAsync(u => u.Username == username);
    if (user is null || !BCrypt.Net.BCrypt.Verify(password, user.Password))
        return Results.Redirect("/login?error=Invalid+username+or+password");

    var claims = new List<Claim>
    {
        new(ClaimTypes.NameIdentifier, user.Id.ToString()),
        new(ClaimTypes.Name, user.Username)
    };
    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
    await ctx.SignInAsync(
        CookieAuthenticationDefaults.AuthenticationScheme,
        new ClaimsPrincipal(identity),
        new AuthenticationProperties { IsPersistent = true, ExpiresUtc = DateTimeOffset.UtcNow.AddDays(30) });

    return Results.Redirect("/");
}).DisableAntiforgery();

app.MapPost("/account/signup", async (HttpContext ctx, AppDbContext db) =>
{
    var form = await ctx.Request.ReadFormAsync();
    var username = form["username"].ToString().Trim();
    var password = form["password"].ToString();
    var confirm  = form["confirm"].ToString();

    if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
        return Results.Redirect("/signup?error=All+fields+are+required");

    if (password != confirm)
        return Results.Redirect("/signup?error=Passwords+do+not+match");

    if (password.Length < 6)
        return Results.Redirect("/signup?error=Password+must+be+at+least+6+characters");

    var taken = await db.Users.AnyAsync(u => u.Username == username);
    if (taken)
        return Results.Redirect("/signup?error=Username+is+already+taken");

    var user = new GratitudeJar.Models.User
    {
        Username = username,
        Password = BCrypt.Net.BCrypt.HashPassword(password),
        FullName = username
    };
    db.Users.Add(user);
    await db.SaveChangesAsync();

    return Results.Redirect("/login?success=Account+created!+Please+log+in.");
}).DisableAntiforgery();

app.MapGet("/account/logout", async (HttpContext ctx) =>
{
    await ctx.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    return Results.Redirect("/login");
});

app.MapRazorComponents<GratitudeJar.Components.App>()
    .AddInteractiveServerRenderMode();

app.Run();
