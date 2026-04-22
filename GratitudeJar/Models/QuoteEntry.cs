using System;

namespace GratitudeJar.Models
{
    public class QuoteEntry : Entry
    {
        private string _author;

        public string Author
        {
            get { return _author; }
            set { _author = value; }
        }

        public QuoteEntry() : base()
        {
            _author = string.Empty;
        }

        public string getAuthor()
        {
            return _author;
        }

        public new void display()
        {
            Console.WriteLine($"[QUOTE] \"{Content}\"");
            Console.WriteLine($"  — {_author} ({Date:yyyy-MM-dd})");
            Console.WriteLine($"  Mood: {MoodTag}");
        }

        public new string summarize()
        {
            string authorShort = string.IsNullOrEmpty(_author) ? "Unknown" : _author;
            if (Content.Length > 40)
                return $"Quote by {authorShort}: \"{Content.Substring(0, 37)}...\"";
            else
                return $"Quote by {authorShort}: \"{Content}\"";
        }
    }
}