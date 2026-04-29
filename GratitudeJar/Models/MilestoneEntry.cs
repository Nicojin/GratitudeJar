using System;

namespace GratitudeJar.Models
{
    public class MilestoneEntry : Entry
    {
        public override void Display()
        {
            Console.WriteLine($"[MILESTONE] {MilestoneTitle} - {Date:yyyy-MM-dd}");
            Console.WriteLine($"  {Content}");
            Console.WriteLine($"  Mood: {MoodTag}");
        }

        public override string Summarize()
        {
            string shortTitle = MilestoneTitle.Length > 20 ? MilestoneTitle.Substring(0, 17) + "..." : MilestoneTitle;
            if (Content.Length > 40)
                return $"Milestone ({shortTitle}): {Content.Substring(0, 37)}...";
            return $"Milestone ({shortTitle}): {Content}";
        }
    }
}