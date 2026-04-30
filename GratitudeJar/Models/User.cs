namespace GratitudeJar.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Pin { get; set; } = string.Empty;
        public int StreakCount { get; set; }

        public User() { }

        public User(string username, string pin)
        {
            Username = username;
            Pin = pin;
            StreakCount = 0;
        }

        public void UpdateStreak() => StreakCount++;
        public int GetStreak() => StreakCount;
        public bool EnterPin(string input) => input == Pin;
    }
}