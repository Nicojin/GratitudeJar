# JarApp – Journaling Application (C# + MariaDB)

## Overview
JarApp is a journaling and mood‑tracking application built in **C# (.NET 6/7)** with a **MariaDB backend**.  
It follows the UML class diagram and flowchart you designed: users can sign up, log in, add entries (Memory, Milestone, Quote), shake the jar for random memories, and view mood insights.

---

## Prerequisites
- [.NET SDK](https://dotnet.microsoft.com/download) (6 or later)
- [MariaDB](https://mariadb.org/download/) installed and running locally
- VS Code with the **C# extension**
- NuGet package: `MySql.Data`

---

## Setup Instructions

1. **Create the project**
   ```bash
   cd "C:\Users\jevau\Downloads\Documents\CODING SHET\C#\JarAppC#"
   dotnet new console -n JarApp
   cd JarApp
   ```

2. **Add MySQL connector**
   ```bash
   dotnet add package MySql.Data
   ```

3. **Copy source files**  
   Place all `.cs` files (`Program.cs`, `JarApp.cs`, `EntryDatabase.cs`, `User.cs`, `Entry.cs`, `MemoryEntry.cs`, `MilestoneEntry.cs`, `QuoteEntry.cs`, `JarUpgrade.cs`, `JarTier.cs`, `IEntryRepository.cs`) into the `JarApp` folder.

4. **Create the database schema with sample data**  
   Save the provided SQL file as `JarAppDB.sql` and import it:
   ```bash
   mysql -u root -p
   SOURCE C:/path/to/JarAppDB.sql;
   ```

5. **Build and run**
   ```bash
   dotnet build
   dotnet run
   ```

---

## How to Use the Program

### 1. Start Menu
- **Sign Up** → Create a new profile with username + PIN. Saved in `Users` table.
- **Login** → Enter username + PIN to access your jar (validated against DB).
- **Exit** → Quit the program.

### 2. Home Menu
- **Entries**
  - Add Entry → Choose type (Memory, Milestone, Quote), fill details, automatically saved to SQL.
  - Browse Entries → View all entries stored in the database.
- **Jar**
  - Shake Jar → Retrieves a random entry from SQL. If empty, prompts to add your first entry.
- **Insights**
  - Shows mood distribution (grouped by `MoodTag`).
  - Displays your current streak count.
- **Exit** → Return to main menu.

### 3. Automatic Saving
- Every time you add an entry, it is immediately inserted into MariaDB via `EntryDatabase.SaveEntry()`.  
- Streaks are updated and persisted in the `Users` table.

---

## Example Run

```
=== JarApp Menu ===
1. Sign Up
2. Login
3. Exit
Choose: 2
Enter username: Alice
Enter PIN: 1234
Login successful!

=== Home ===
1. Entries
2. Jar
3. Insights
4. Exit
Choose: 1
=== Entries ===
1. Add Entry
2. Browse Entries
Choose: 2
Memory: Trip to the beach (Image: beach.png)
Milestone: Graduation - Graduated college today!
Quote by Unknown: Stay positive!
```

---

## Insights Example
```
=== Insights ===
Relaxed: 1
Motivated: 2
Happy: 1
Proud: 1
Current streak: 2
```

---

## Test Accounts
After importing `JarAppDB.sql`, you can log in immediately with:
- Alice → PIN: 1234
- Bob → PIN: 5678

Both have sample entries preloaded for testing.
