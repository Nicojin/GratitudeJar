namespace GratitudeJar.Models;

public class Entry
{
    public int EntryId { get; set; }
    public int UserId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTime EntryDate { get; set; }
    public string Mood { get; set; } = string.Empty;
    public string MoodFlag { get; set; } = string.Empty;
    public bool IsDeleted { get; set; } = false;
    public User User { get; set; } = null!;
}
