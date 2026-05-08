namespace GratitudeJar.Models;

public class MilestoneEntry : Entry
{
    public string Milestone { get; set; } = string.Empty;

    public string GetMoodEmoji() => MoodFlag ?? "🏆";
    public string GetDate() => EntryDate.ToString("MMMM d, yyyy");
}
