using Microsoft.EntityFrameworkCore;
using GratitudeJar.Interfaces;
using GratitudeJar.Models;

namespace GratitudeJar.Data;

public class EntryDatabase : IEntryRepository
{
    private readonly AppDbContext _db;

    public EntryDatabase(AppDbContext db)
    {
        _db = db;
    }

    public async Task<List<Entry>> GetAll(int userId)
    {
        return await _db.Entries
            .Where(e => e.UserId == userId)
            .OrderByDescending(e => e.EntryDate)
            .ToListAsync();
    }

    public async Task<Entry?> GetById(int id, int userId)
    {
        return await _db.Entries
            .FirstOrDefaultAsync(e => e.EntryId == id && e.UserId == userId);
    }

    public async Task Add(Entry entry)
    {
        _db.Entries.Add(entry);
        await _db.SaveChangesAsync();
    }

    public async Task Update(Entry entry)
    {
        _db.Entries.Update(entry);
        await _db.SaveChangesAsync();
    }

    public async Task Delete(int id, int userId)
    {
        var entry = await _db.Entries
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(e => e.EntryId == id && e.UserId == userId);
        if (entry is not null)
        {
            entry.IsDeleted = true;
            await _db.SaveChangesAsync();
        }
    }

    public string GetEntryType(Entry entry) => entry switch
    {
        MemoryEntry    => "Memory",
        MilestoneEntry => "Milestone",
        QuoteEntry     => "Quote",
        _              => "Entry"
    };
}
