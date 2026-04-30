using System.Collections.Generic;
using GratitudeJar.Models;

namespace GratitudeJar.Data
{
    public interface IEntryRepository
    {
        void SaveEntry(Entry entry);
        void DeleteEntry(int id);
        List<Entry> GetAllEntries();
        Entry GetRandomEntry();
        int GetEntryCount();
        void SaveUser(User user);
        User? GetUser(string username);
        User? GetUserById(int id);
    }
}