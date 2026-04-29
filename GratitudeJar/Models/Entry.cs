using System;

namespace GratitudeJar.Models
{
    public class Entry
    {
        public int EntryId { get; set; }
        public string Content { get; set; } = string.Empty;
        public DateTime Date { get; set; } = DateTime.Now;
        public string MoodTag { get; set; } = string.Empty;
        public int UserId { get; set; }

        public string Image { get; set; } = string.Empty;
        public string MilestoneTitle { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;

        public virtual void Display()
        {
            Console.WriteLine($"[{Date:yyyy-MM-dd}] {Content} (Mood: {MoodTag})");
        }

        public virtual string Summarize()
        {
            if (Content.Length > 50)
                return $"{Date:MM/dd}: {Content.Substring(0, 47)}...";
            return $"{Date:MM/dd}: {Content}";
        }
    }
}