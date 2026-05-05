using System;

public class Entry
{
    public int EntryId { get; set; }
    public required string Content { get; set; }
    public DateTime Date { get; set; }
    public required string MoodTag { get; set; }
    public int UserId { get; set; }

    public int GetId() => EntryId;
    public string GetContent() => Content;
    public virtual void Display() => Console.WriteLine($"{Date}: {Content} [{MoodTag}]");
    public virtual string Summarize() => Content.Substring(0, Math.Min(50, Content.Length));
}
