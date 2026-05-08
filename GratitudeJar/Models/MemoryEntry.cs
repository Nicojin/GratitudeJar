namespace GratitudeJar.Models;

public class MemoryEntry : Entry
{
    public string ImagePath { get; set; } = string.Empty;

    public string GetPhoto() => ImagePath ?? string.Empty;
    public string GetMoodEmoji() => MoodFlag ?? "😊";
    public string GetDate() => EntryDate.ToString("MMMM d, yyyy");
}
