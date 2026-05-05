public class User
{
    public int UserId { get; set; }
    public string Username { get; set; }
    public string Pin { get; private set; }
    public int StreakCount { get; set; }

    public User(string uname, string p, int streak = 0)
    {
        Username = uname;
        Pin = p;
        StreakCount = streak;
    }

    public void UpdateStreak() => StreakCount++;
    public int GetStreak() => StreakCount;
    public bool EnterPin(string input) => input == Pin;
}
