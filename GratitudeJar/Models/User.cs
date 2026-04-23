using System;

namespace GratitudeJar.Models
{
    public class User
    {
        private string _username;
        private string _pin;
        private int _streakCount;
        private DateTime _lastEntryDate;

        public string Username
        {
            get { return _username; }
            set { _username = value; }
        }

        public int StreakCount
        {
            get { return _streakCount; }
            set { _streakCount = value; }
        }

        public DateTime LastEntryDate
        {
            get { return _lastEntryDate; }
            set { _lastEntryDate = value; }
        }

        public User()
        {
            _username = string.Empty;
            _pin = string.Empty;
            _streakCount = 0;
            _lastEntryDate = DateTime.MinValue;
        }
        public string getUsername()
        {
            return _username;
        }

        public bool verifyPin(string inputPin)
        {
            return _pin == inputPin;
        }

        public void setPin(string newPin)
        {
            _pin = newPin;
        }

        public void updateStreak()
        {
            DateTime today = DateTime.Now.Date;

            if (_lastEntryDate == DateTime.MinValue)
            {
                _streakCount = 1;
            }
            else if (_lastEntryDate.Date == today)
            {
                return;
            }
            else if (_lastEntryDate.Date == today.AddDays(-1))
            {
                _streakCount++;
            }
            else
            {
                _streakCount = 1;
            }

            _lastEntryDate = today;
        }

        public int getStreak()
        {
            return _streakCount;
        }
        public string GetPinHash()
        {
            return _pin;
        }
    }
}