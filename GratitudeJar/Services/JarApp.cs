using GratitudeJar.Models;

namespace GratitudeJar.Services;

public class JarApp
{
    public User Profile { get; set; } = null!;
    public List<Entry> Entries { get; set; } = new();

    public void AddEntry(Entry entry) => Entries.Add(entry);
    public void RemoveEntry(Entry entry) => Entries.Remove(entry);
    public Entry? GetEntry(int entryId) => Entries.FirstOrDefault(e => e.EntryId == entryId);

    public int GetStreak()
    {
        var dates = Entries.Select(e => e.EntryDate.Date).Distinct().ToHashSet();
        int streak = 0;
        var day = DateTime.UtcNow.Date;
        while (dates.Contains(day))
        {
            streak++;
            day = day.AddDays(-1);
        }
        return streak;
    }

    public int GetJarFillPercent()
    {
        var count = Math.Min(Entries.Count, 50);
        return (int)((count / 50.0) * 100);
    }

    public Entry? GetRandomEntry()
    {
        if (Entries.Count == 0) return null;
        return Entries[Random.Shared.Next(Entries.Count)];
    }
}
