using System;
using System.Linq;

class Program
{
    static void Main()
    {
        string connStr = "server=localhost;user=root;password=;database=JarAppDB;";
        IEntryRepository repo;
        try
        {
            repo = new EntryDatabase(connStr);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Database connection failed: {ex.Message}");
            Console.WriteLine("Please ensure MySQL is running and the database exists.");
            return;
        }
        User? user = null;
        JarApp? app = null;

        Console.WriteLine("JarApp started successfully.");
        Console.WriteLine("Press Enter to continue...");
        Console.ReadLine();
        while (true)
        {
            Console.Clear();
            Console.WriteLine("\n=== JarApp Menu ===");
            Console.WriteLine("1. Sign Up");
            Console.WriteLine("2. Login");
            Console.WriteLine("3. Exit");
            Console.Write("Choose: ");
            var choice = Console.ReadLine();

            if (choice == null) break;

            if (choice == "1")
            {
                Console.Write("Enter username: ");
                string? uname = Console.ReadLine();
                if (uname == null) break;
                Console.Write("Enter PIN: ");
                string? pin = Console.ReadLine();
                if (pin == null) break;
                user = new User(uname, pin);
                repo.SaveUser(user);
                Console.WriteLine("Profile created!");
                Console.WriteLine("Press Enter to continue...");
                Console.ReadLine();
            }
            else if (choice == "2")
            {
                Console.Write("Enter username: ");
                string? uname = Console.ReadLine();
                if (uname == null) break;
                Console.Write("Enter PIN: ");
                string? pin = Console.ReadLine();
                if (pin == null) break;

                user = repo.GetUser(uname); // DB-backed login
                if (user.UserId != -1 && user.EnterPin(pin))
                {
                    app = new JarApp(user, repo);
                    Console.WriteLine("Login successful!");
                    HomeMenu(app, user);
                    Console.WriteLine("Press Enter to continue...");
                    Console.ReadLine();
                }
                else
                {
                    Console.WriteLine("Login failed. Invalid username or PIN.");
                }
            }
            else if (choice == "3")
            {
                Console.WriteLine("Goodbye!");
                break;
            }
        }
    }

    static void HomeMenu(JarApp app, User user)
    {
        while (true)
        {
            Console.Clear(); // clear before showing menu
            Console.WriteLine("\n=== Home ===");
            Console.WriteLine("1. Entries");
            Console.WriteLine("2. Jar");
            Console.WriteLine("3. Insights");
            Console.WriteLine("4. Exit");
            Console.Write("Choose: ");
            var choice = Console.ReadLine();

            if (choice == null) break;

            if (choice == "1") EntriesMenu(app, user);
            else if (choice == "2") JarMenu(app);
            else if (choice == "3") InsightsMenu(app, user);
            else if (choice == "4") break;
        }
    }

    static void EntriesMenu(JarApp app, User user)
    {
        Console.Clear(); // clear before showing menu
        Console.WriteLine("\n=== Entries ===");
        Console.WriteLine("1. Add Entry");
        Console.WriteLine("2. Browse Entries");
        Console.Write("Choose: ");
        var choice = Console.ReadLine();

        if (choice == null) return;

        if (choice == "1")
        {
            Console.WriteLine("Choose type: 1=Memory, 2=Milestone, 3=Quote");
            string? type = Console.ReadLine();
            if (type == null) return;
            Console.Write("Content: ");
            string? content = Console.ReadLine();
            if (content == null) return;
            Console.Write("Mood: ");
            string? mood = Console.ReadLine();
            if (mood == null) return;

            Entry entry;
            if (type == "1")
            {
                Console.Write("Image filename: ");
                string? img = Console.ReadLine();
                if (img == null) return;
                entry = new MemoryEntry { Content = content, Date = DateTime.Now, MoodTag = mood, Image = img, UserId = user.UserId };
            }
            else if (type == "2")
            {
                Console.Write("Milestone: ");
                string? ms = Console.ReadLine();
                if (ms == null) return;
                entry = new MilestoneEntry { Content = content, Date = DateTime.Now, MoodTag = mood, Milestone = ms, UserId = user.UserId };
            }
            else
            {
                Console.Write("Author: ");
                string? author = Console.ReadLine();
                if (author == null) return;
                entry = new QuoteEntry { Content = content, Date = DateTime.Now, MoodTag = mood, Author = author, UserId = user.UserId };
            }

            app.AddEntry(entry); // Automatically saves to SQL
            Console.WriteLine("Entry saved!");
            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
        }
        else if (choice == "2")
        {
            var entries = app.BrowseEntries();
            foreach (var e in entries) e.Display();
            Console.WriteLine("Press Enter to continue...");
        Console.ReadLine();
        }
    }

    static void JarMenu(JarApp app)
    {
        while (true)
        {
            Console.Clear(); // clear before showing menu
            Console.WriteLine("\n=== Jar ===");
            Console.WriteLine("1. Shake Jar");
            Console.WriteLine("2. Exit Jar");
            Console.Write("Choose: ");
            var choice = Console.ReadLine();

            if (choice == "1")
            {
                var entry = app.ShakeJar();
                if (entry.Content == "Jar is empty. Add your first entry!")
                {
                    Console.WriteLine("Jar is empty. Add your first entry!");
                    Console.WriteLine("Press Enter to continue...");
                    Console.ReadLine();
                }
                else
                {
                    Console.WriteLine("\n--- Random Entry ---");
                    Console.WriteLine(entry.Summarize()); // or entry.Display()
                    Console.WriteLine("Press Enter to continue...");
                    Console.ReadLine();
                }
            }
            else if (choice == "2")
            {
                break; // return to Home menu
            }
        }
    }


    static void InsightsMenu(JarApp app, User user)
    {
        Console.WriteLine("\n=== Insights ===");
        app.ShowMoodChart();
        Console.WriteLine($"Current streak: {user.GetStreak()}");
        Console.WriteLine("Press Enter to continue...");
        Console.ReadLine();
    }
}
