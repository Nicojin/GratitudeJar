namespace GratitudeJar.Models;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string Bio { get; set; } = string.Empty;
    public string ProfilePic { get; set; } = string.Empty;
    public List<Entry> Entries { get; set; } = new();
}
