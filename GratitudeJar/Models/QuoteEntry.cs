using System;

namespace GratitudeJar.Models
{
    public class QuoteEntry : Entry
    {
        public override void Display()
        {
            Console.WriteLine($"[QUOTE] \"{Content}\"");
            Console.WriteLine($"  — {Author} ({Date:yyyy-MM-dd})");
            Console.WriteLine($"  Mood: {MoodTag}");
        }

        public override string Summarize()
        {
            string shortAuthor = string.IsNullOrEmpty(Author) ? "Unknown" : Author;
            if (Content.Length > 40)
                return $"Quote by {shortAuthor}: \"{Content.Substring(0, 37)}...\"";
            return $"Quote by {shortAuthor}: \"{Content}\"";
        }
    }
}