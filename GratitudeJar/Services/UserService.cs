using Microsoft.EntityFrameworkCore;
using GratitudeJar.Data;
using GratitudeJar.Models;

namespace GratitudeJar.Services;

public class UserService
{
    private readonly AppDbContext _db;

    public UserService(AppDbContext db) => _db = db;

    public async Task<(bool Success, string Error)> UpdateProfileAsync(
        int userId, string fullName, string username, string bio, string? profilePic)
    {
        username = username.Trim();
        if (string.IsNullOrWhiteSpace(username))
            return (false, "Username cannot be empty.");

        var taken = await _db.Users.AnyAsync(u => u.Username == username && u.Id != userId);
        if (taken)
            return (false, "Username is already taken.");

        var user = await _db.Users.FindAsync(userId);
        if (user is null) return (false, "User not found.");

        user.FullName = fullName.Trim();
        user.Username = username;
        user.Bio = bio.Trim();
        if (profilePic is not null)
            user.ProfilePic = profilePic;

        await _db.SaveChangesAsync();
        return (true, string.Empty);
    }

    public async Task<(bool Success, string Error)> ChangePasswordAsync(
        int userId, string currentPassword, string newPassword)
    {
        var user = await _db.Users.FindAsync(userId);
        if (user is null) return (false, "User not found.");

        if (!BCrypt.Net.BCrypt.Verify(currentPassword, user.Password))
            return (false, "Current password is incorrect.");

        if (newPassword.Length < 6)
            return (false, "New password must be at least 6 characters.");

        user.Password = BCrypt.Net.BCrypt.HashPassword(newPassword);
        await _db.SaveChangesAsync();
        return (true, string.Empty);
    }
}
