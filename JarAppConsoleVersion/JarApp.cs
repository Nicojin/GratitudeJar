using System.Collections.Generic;

public class JarApp
{
    private User profile;
    private IEntryRepository database;
    private JarTier jarTier;
    private int entryCount;

    public JarApp(User user, IEntryRepository repo)
    {
        profile = user;
        database = repo;
        jarTier = JarTier.Glass;
        entryCount = database.GetEntryCount();
    }

    public void AddEntry(Entry entry)
    {
        entry.UserId = profile.UserId;
        database.SaveEntry(entry);       // auto-save to SQL
        entryCount++;
        profile.UpdateStreak();
        database.SaveUser(profile);
        jarTier = new JarUpgrade(jarTier, entryCount).ApplyUpgrade(entryCount);
    }

    public List<Entry> BrowseEntries() => database.GetAllEntries();
    public Entry ShakeJar() => database.GetRandomEntry();

    public void ShowMoodChart()
    {
        var entries = database.GetAllEntries();
        var moodGroups = entries.GroupBy(e => e.MoodTag)
                                .Select(g => new { Mood = g.Key, Count = g.Count() });
        foreach (var mood in moodGroups)
            Console.WriteLine($"{mood.Mood}: {mood.Count}");
    }

    public void UpgradeJar(JarTier tier) => jarTier = tier;
}
