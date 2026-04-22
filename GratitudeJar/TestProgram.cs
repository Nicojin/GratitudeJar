using GratitudeJar.Models;
using System;
using System.Collections.Generic;

namespace GratitudeJar
{
    public class TestProgram
    {
        public static void RunTest()
        {
            Console.WriteLine("=== Testing Entry Classes ===\n");

            List<Entry> myJar = new List<Entry>();

            Console.WriteLine("--- Adding Entries ---");

            Entry regularEntry = new Entry();
            regularEntry.Content = "Grateful for good weather today";
            regularEntry.MoodTag = "Happy";
            myJar.Add(regularEntry);
            Console.WriteLine("✓ Added regular entry");

            MemoryEntry memory = new MemoryEntry();
            memory.Content = "Coffee with best friend at our favorite cafe";
            memory.MoodTag = "Joyful";
            memory.Image = "cafe_memory.jpg";
            myJar.Add(memory);
            Console.WriteLine("✓ Added memory entry");

            MilestoneEntry milestone = new MilestoneEntry();
            milestone.Content = "Completed my first week of gratitude journaling";
            milestone.MoodTag = "Proud";
            milestone.Milestone = "7-Day Streak";
            myJar.Add(milestone);
            Console.WriteLine("✓ Added milestone entry");

            QuoteEntry quote = new QuoteEntry();
            quote.Content = "Gratitude turns what we have into enough";
            quote.MoodTag = "Inspired";
            quote.Author = "Anonymous";
            myJar.Add(quote);
            Console.WriteLine("✓ Added quote entry");

            Console.WriteLine("\n--- All Entries in Jar ---");
            foreach (Entry entry in myJar)
            {
                entry.display();
                Console.WriteLine($"  Summary: {entry.summarize()}");
                Console.WriteLine();
            }

            Console.WriteLine("--- Direct Entry Creation (Not Abstract) ---");
            Entry directEntry = new Entry();
            directEntry.Content = "I can create an Entry without using Memory/Milestone/Quote";
            directEntry.MoodTag = "Grateful";
            directEntry.display();
            Console.WriteLine($"Summary: {directEntry.summarize()}");

            Console.WriteLine("\n=== Test Complete ===");
        }
    }
}