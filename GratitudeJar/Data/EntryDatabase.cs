using System;
using System.Collections.Generic;
using System.Linq;
using GratitudeJar.Models;

namespace GratitudeJar.Data
{
    public class EntryDatabase : IEntryRepository
    {
        private List<Entry> _entries = new List<Entry>();
        private List<User> _users = new List<User>();
        private int _nextEntryId = 1;
        private int _nextUserId = 1;

        public void SaveEntry(Entry entry)
        {
            if (entry.EntryId == 0)
            {
                entry.EntryId = _nextEntryId++;
                _entries.Add(entry);
            }
            else
            {
                var existing = _entries.FirstOrDefault(e => e.EntryId == entry.EntryId);
                if (existing != null)
                {
                    existing.Content = entry.Content;
                    existing.MoodTag = entry.MoodTag;
                    existing.Date = entry.Date;
                }
            }
        }

        public void DeleteEntry(int id)
        {
            _entries.RemoveAll(e => e.EntryId == id);
        }

        public List<Entry> GetAllEntries()
        {
            return _entries.ToList();
        }

        public Entry GetRandomEntry()
        {
            if (_entries.Count == 0)
                return new Entry { Content = "Jar is empty. Add your first entry!" };

            Random rand = new Random();
            return _entries[rand.Next(_entries.Count)];
        }

        public int GetEntryCount()
        {
            return _entries.Count;
        }

        // User
        public void SaveUser(User user)
        {
            var existing = _users.FirstOrDefault(u => u.Username == user.Username);
            if (existing == null)
            {
                user.UserId = _nextUserId++;
                _users.Add(user);
            }
            else
            {
                existing.Pin = user.Pin;
                existing.StreakCount = user.StreakCount;
            }
        }

        public User? GetUser(string username)
        {
            return _users.FirstOrDefault(u => u.Username == username);
        }

        public User? GetUserById(int id)
        {
            return _users.FirstOrDefault(u => u.UserId == id);
        }
    }
}