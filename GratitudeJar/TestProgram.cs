using GratitudeJar.Models;
using System;
using System.Collections.Generic;

namespace GratitudeJar
{
    public class TestProgram
    {
        public static void RunTest()
        {
            Console.WriteLine("Testing Entry Classes\n");

            List<Entry> myJar = new List<Entry>();

            Entry regularEntry = new Entry();
            regularEntry.Content = "Happy it's cold";
            regularEntry.MoodTag = "Happy";
            myJar.Add(regularEntry);

            MemoryEntry memory = new MemoryEntry();
            memory.Content = "Coffee with Jev & Julian";
            memory.MoodTag = "Joyful";
            memory.Image = "cafe_memory.jpg";
            myJar.Add(memory);

            MilestoneEntry milestone = new MilestoneEntry();
            milestone.Content = "Completed 1km run";
            milestone.MoodTag = "Proud";
            milestone.Milestone = "Workout";
            myJar.Add(milestone);

            QuoteEntry quote = new QuoteEntry();
            quote.Content = "One day or day one";
            quote.MoodTag = "Inspired";
            quote.Author = "Unknown";
            myJar.Add(quote);

            Console.WriteLine("All Entries\n");
            foreach (Entry entry in myJar)
            {
                entry.display();
                Console.WriteLine();
            }
        }
    }
}