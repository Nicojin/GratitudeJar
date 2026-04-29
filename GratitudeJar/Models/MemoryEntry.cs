using System;

namespace GratitudeJar.Models
{
    public class MemoryEntry : Entry
    {
        public override void Display()
        {
            Console.WriteLine($"[MEMORY] {Date:yyyy-MM-dd}: {Content}");
            if (!string.IsNullOrEmpty(Image))
                Console.WriteLine($"  Image: {Image}");
            Console.WriteLine($"  Mood: {MoodTag}");
        }

        public override string Summarize()
        {
            string indicator = string.IsNullOrEmpty(Image) ? "" : " [image]";
            if (Content.Length > 50)
                return $"Memory{indicator} {Date:MM/dd}: {Content.Substring(0, 47)}...";
            return $"Memory{indicator} {Date:MM/dd}: {Content}";
        }
    }
}