using System.Collections.Generic;

public interface IEntryRepository
{
    void SaveEntry(Entry entry);
    void EditEntry(Entry entry);
    void DeleteEntry(int id);
    List<Entry> GetAllEntries();
    Entry GetRandomEntry();
    void SaveUser(User user);
    int GetEntryCount();
    User GetUser(string username);
}
