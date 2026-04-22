using System;

namespace GratitudeJar.Models
{
    public class MilestoneEntry : Entry
    {
        private string _milestone;

        public string Milestone
        {
            get { return _milestone; }
            set { _milestone = value; }
        }

        public MilestoneEntry() : base()
        {
            _milestone = string.Empty;
        }

        public string getMilestone()
        {
            return _milestone;
        }

        public new void display()
        {
            Console.WriteLine($"[MILESTONE] {_milestone} - {Date:yyyy-MM-dd}");
            Console.WriteLine($"  {Content}");
            Console.WriteLine($"  Mood: {MoodTag}");
        }

        public new string summarize()
        {
            string milestoneShort = _milestone.Length > 20 ? _milestone.Substring(0, 17) + "..." : _milestone;
            if (Content.Length > 40)
                return $"Milestone ({milestoneShort}): {Content.Substring(0, 37)}...";
            else
                return $"Milestone ({milestoneShort}): {Content}";
        }
    }
}