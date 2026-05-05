using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

public class EntryDatabase : IEntryRepository
{
    private readonly string connectionString;
    public EntryDatabase(string connStr)
    {
        connectionString = connStr;
        EnsureTableExists();
    }

    private void EnsureTableExists()
    {
        using var conn = new MySqlConnection(connectionString);
        conn.Open();
        string sql = @"
CREATE TABLE IF NOT EXISTS Entries (
    EntryId INT AUTO_INCREMENT PRIMARY KEY,
    Content TEXT,
    Date DATETIME,
    MoodTag VARCHAR(255),
    EntryType VARCHAR(255),
    Image TEXT,
    Milestone TEXT,
    Author VARCHAR(255),
    UserId INT
);
CREATE TABLE IF NOT EXISTS Users (
    UserId INT AUTO_INCREMENT PRIMARY KEY,
    Username VARCHAR(255) UNIQUE,
    Pin VARCHAR(255),
    StreakCount INT
);";
        using var cmd = new MySqlCommand(sql, conn);
        cmd.ExecuteNonQuery();
    }

    public void SaveEntry(Entry entry)
    {
        using var conn = new MySqlConnection(connectionString);
        conn.Open();
        string sql = @"INSERT INTO Entries 
            (Content, Date, MoodTag, EntryType, Image, Milestone, Author, UserId) 
            VALUES (@content,@date,@mood,@type,@image,@milestone,@author,@userId)";
        using var cmd = new MySqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@content", entry.Content);
        cmd.Parameters.AddWithValue("@date", entry.Date);
        cmd.Parameters.AddWithValue("@mood", entry.MoodTag);
        cmd.Parameters.AddWithValue("@type", entry.GetType().Name);
        cmd.Parameters.AddWithValue("@image", (entry is MemoryEntry me) ? me.Image : null);
        cmd.Parameters.AddWithValue("@milestone", (entry is MilestoneEntry ms) ? ms.Milestone : null);
        cmd.Parameters.AddWithValue("@author", (entry is QuoteEntry qe) ? qe.Author : null);
        cmd.Parameters.AddWithValue("@userId", entry.UserId);
        cmd.ExecuteNonQuery();
    }

    public void EditEntry(Entry entry)
    {
        using var conn = new MySqlConnection(connectionString);
        conn.Open();
        string sql = @"UPDATE Entries SET 
            Content=@content, Date=@date, MoodTag=@mood, EntryType=@type, Image=@image, Milestone=@milestone, Author=@author, UserId=@userId
            WHERE EntryId=@id";
        using var cmd = new MySqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@id", entry.EntryId);
        cmd.Parameters.AddWithValue("@content", entry.Content);
        cmd.Parameters.AddWithValue("@date", entry.Date);
        cmd.Parameters.AddWithValue("@mood", entry.MoodTag);
        cmd.Parameters.AddWithValue("@type", entry.GetType().Name);
        cmd.Parameters.AddWithValue("@image", (entry is MemoryEntry me) ? me.Image : null);
        cmd.Parameters.AddWithValue("@milestone", (entry is MilestoneEntry ms) ? ms.Milestone : null);
        cmd.Parameters.AddWithValue("@author", (entry is QuoteEntry qe) ? qe.Author : null);
        cmd.Parameters.AddWithValue("@userId", entry.UserId);
        cmd.ExecuteNonQuery();
    }

    public void DeleteEntry(int id)
    {
        using var conn = new MySqlConnection(connectionString);
        conn.Open();
        string sql = "DELETE FROM Entries WHERE EntryId=@id";
        using var cmd = new MySqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@id", id);
        cmd.ExecuteNonQuery();
    }

    public List<Entry> GetAllEntries()
    {
        var entries = new List<Entry>();
        using var conn = new MySqlConnection(connectionString);
        conn.Open();
        string sql = "SELECT * FROM Entries";
        using var cmd = new MySqlCommand(sql, conn);
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            string type = reader.GetString("EntryType");
            Entry e;
            if (type == nameof(MemoryEntry))
            {
                e = new MemoryEntry { Content = reader.GetString("Content"), Date = reader.GetDateTime("Date"), MoodTag = reader.GetString("MoodTag"), Image = reader.IsDBNull(reader.GetOrdinal("Image")) ? "" : reader.GetString("Image"), UserId = reader.GetInt32("UserId") };
            }
            else if (type == nameof(MilestoneEntry))
            {
                e = new MilestoneEntry { Content = reader.GetString("Content"), Date = reader.GetDateTime("Date"), MoodTag = reader.GetString("MoodTag"), Milestone = reader.IsDBNull(reader.GetOrdinal("Milestone")) ? "" : reader.GetString("Milestone"), UserId = reader.GetInt32("UserId") };
            }
            else if (type == nameof(QuoteEntry))
            {
                e = new QuoteEntry { Content = reader.GetString("Content"), Date = reader.GetDateTime("Date"), MoodTag = reader.GetString("MoodTag"), Author = reader.IsDBNull(reader.GetOrdinal("Author")) ? "" : reader.GetString("Author"), UserId = reader.GetInt32("UserId") };
            }
            else
            {
                e = new Entry { Content = reader.GetString("Content"), Date = reader.GetDateTime("Date"), MoodTag = reader.GetString("MoodTag"), UserId = reader.GetInt32("UserId") };
            }
            entries.Add(e);
        }
        return entries;
    }

    public Entry GetRandomEntry()
    {
        using var conn = new MySqlConnection(connectionString);
        conn.Open();
        string sql = "SELECT * FROM Entries ORDER BY RAND() LIMIT 1";
        using var cmd = new MySqlCommand(sql, conn);
        using var reader = cmd.ExecuteReader();
        if (reader.Read())
        {
            string type = reader.GetString("EntryType");
            if (type == nameof(MemoryEntry))
            {
                return new MemoryEntry { Content = reader.GetString("Content"), Date = reader.GetDateTime("Date"), MoodTag = reader.GetString("MoodTag"), Image = reader.IsDBNull(reader.GetOrdinal("Image")) ? "" : reader.GetString("Image"), UserId = reader.GetInt32("UserId") };
            }
            else if (type == nameof(MilestoneEntry))
            {
                return new MilestoneEntry { Content = reader.GetString("Content"), Date = reader.GetDateTime("Date"), MoodTag = reader.GetString("MoodTag"), Milestone = reader.IsDBNull(reader.GetOrdinal("Milestone")) ? "" : reader.GetString("Milestone"), UserId = reader.GetInt32("UserId") };
            }
            else if (type == nameof(QuoteEntry))
            {
                return new QuoteEntry { Content = reader.GetString("Content"), Date = reader.GetDateTime("Date"), MoodTag = reader.GetString("MoodTag"), Author = reader.IsDBNull(reader.GetOrdinal("Author")) ? "" : reader.GetString("Author"), UserId = reader.GetInt32("UserId") };
            }
            else
            {
                return new Entry { Content = reader.GetString("Content"), Date = reader.GetDateTime("Date"), MoodTag = reader.GetString("MoodTag"), UserId = reader.GetInt32("UserId") };
            }
        }
        return new Entry { Content = "Jar is empty. Add your first entry!", Date = DateTime.Now, MoodTag = "Empty", UserId = 0 };
    }
    public void SaveUser(User user)
    {
        using var conn = new MySqlConnection(connectionString);
        conn.Open();
        string sql = @"INSERT INTO Users (Username, Pin, StreakCount) 
                       VALUES (@uname, @pin, @streak) 
                       ON DUPLICATE KEY UPDATE Pin=@pin, StreakCount=@streak";
        using var cmd = new MySqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@uname", user.Username);
        cmd.Parameters.AddWithValue("@pin", user.Pin);
        cmd.Parameters.AddWithValue("@streak", user.StreakCount);
        cmd.ExecuteNonQuery();
        if (user.UserId == 0)
        {
            cmd.CommandText = "SELECT UserId FROM Users WHERE Username=@uname";
            user.UserId = Convert.ToInt32(cmd.ExecuteScalar());
        }
    }

    public int GetEntryCount()
    {
        using var conn = new MySqlConnection(connectionString);
        conn.Open();
        string sql = "SELECT COUNT(*) FROM Entries";
        using var cmd = new MySqlCommand(sql, conn);
        return Convert.ToInt32(cmd.ExecuteScalar());
    }

    public User GetUser(string username)
    {
        using var conn = new MySqlConnection(connectionString);
        conn.Open();
        string sql = "SELECT UserId, Username, Pin, StreakCount FROM Users WHERE Username=@uname";
        using var cmd = new MySqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@uname", username);
        using var reader = cmd.ExecuteReader();
        if (reader.Read())
        {
            return new User(reader.GetString("Username"), reader.GetString("Pin"), reader.GetInt32("StreakCount")) { UserId = reader.GetInt32("UserId") };
        }
        return new User("", "", 0) { UserId = -1 };
    }
}