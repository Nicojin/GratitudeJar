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

        public override void display()
        {
            Console.WriteLine($"{Date:yyyy-MM-dd} - {_milestone}");
            Console.WriteLine($"  {Content}");
            Console.WriteLine($"  Mood: {MoodTag}");
        }

        public override string summarize()
        {
            string milestoneShort = _milestone.Length > 20 ? _milestone.Substring(0, 17) + "..." : _milestone;
            if (Content.Length > 40)
                return $"Milestone ({milestoneShort}): {Content.Substring(0, 37)}...";
            else
                return $"Milestone ({milestoneShort}): {Content}";
        }
    }
}