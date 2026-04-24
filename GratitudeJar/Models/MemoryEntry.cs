using System;

namespace GratitudeJar.Models
{
    public class MemoryEntry : Entry
    {
        private string _image;

        public string Image
        {
            get { return _image; }
            set { _image = value; }
        }

        public MemoryEntry() : base()
        {
            _image = string.Empty;
        }

        public string getImage()
        {
            return _image;
        }

        public override void display()
        {
            Console.WriteLine($"{Date:yyyy-MM-dd} - {Content}");
            if (!string.IsNullOrEmpty(_image))
                Console.WriteLine($"  Image: {_image}");
            Console.WriteLine($"  Mood: {MoodTag}");
        }

        public override string summarize()
        {
            string imageIndicator = string.IsNullOrEmpty(_image) ? "" : " [has image]";
            if (Content.Length > 50)
                return $"Memory{imageIndicator} {Date:MM/dd}: {Content.Substring(0, 47)}...";
            else
                return $"Memory{imageIndicator} {Date:MM/dd}: {Content}";
        }
    }
}