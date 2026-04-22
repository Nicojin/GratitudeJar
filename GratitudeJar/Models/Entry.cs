using System;

namespace GratitudeJar.Models
{
    public class Entry
    {
        private int _entryId;
        private string _content;
        private DateTime _date;
        private string _moodTag;

        public int EntryId
        {
            get { return _entryId; }
            set { _entryId = value; }
        }

        public string Content
        {
            get { return _content; }
            set { _content = value; }
        }

        public DateTime Date
        {
            get { return _date; }
            set { _date = value; }
        }

        public string MoodTag
        {
            get { return _moodTag; }
            set { _moodTag = value; }
        }

        public Entry()
        {
            _date = DateTime.Now;
            _moodTag = "grateful";
            _content = string.Empty;
        }

        public int getId()
        {
            return _entryId;
        }

        public string getContent()
        {
            return _content;
        }

        public string getMoodTag()
        {
            return _moodTag;
        }

        public virtual void display()
        {
            Console.WriteLine($"[ENTRY] {_date:yyyy-MM-dd} - {_content} ");
            Console.WriteLine($"(Mood: {_moodTag})");
        }

        public virtual string summarize()
        {
            if (_content.Length > 50)
                return $"{_date:MM/dd}: {_content.Substring(0, 47)}...";
            else
                return $"{_date:MM/dd}: {_content}";
        }
    }
}