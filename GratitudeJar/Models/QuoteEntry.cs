namespace GratitudeJar.Models;

public class QuoteEntry : Entry
{
    public string Author { get; set; } = string.Empty;

    public string GetMoodEmoji() => MoodFlag ?? "💬";
    public string GetDate() => EntryDate.ToString("MMMM d, yyyy");
}
