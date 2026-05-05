-- JarAppDB.sql
-- Schema + sample data for JarApp journaling application

CREATE DATABASE IF NOT EXISTS JarAppDB;
USE JarAppDB;

-- Users table
CREATE TABLE IF NOT EXISTS Users (
    UserId INT AUTO_INCREMENT PRIMARY KEY,
    Username VARCHAR(100) NOT NULL UNIQUE,
    Pin VARCHAR(20) NOT NULL,
    StreakCount INT DEFAULT 0
);

-- Entries table
CREATE TABLE IF NOT EXISTS Entries (
    EntryId INT AUTO_INCREMENT PRIMARY KEY,
    Content TEXT NOT NULL,
    Date DATETIME NOT NULL,
    MoodTag VARCHAR(50),
    EntryType VARCHAR(20),   -- "MemoryEntry", "MilestoneEntry", "QuoteEntry"
    Image TEXT,              -- for MemoryEntry
    Milestone VARCHAR(255),  -- for MilestoneEntry
    Author VARCHAR(100),     -- for QuoteEntry
    UserId INT,
    FOREIGN KEY (UserId) REFERENCES Users(UserId)
);

-- Index for faster mood queries
CREATE INDEX idx_moodtag ON Entries(MoodTag);

-- ---------------------------------------------------
-- Sample data (automatically added when file is run)
-- ---------------------------------------------------

-- Insert sample users
INSERT INTO Users (Username, Pin, StreakCount) VALUES
('Alice', '1234', 2),
('Bob', '5678', 1);

-- Insert sample entries for Alice (UserId = 1)
INSERT INTO Entries (Content, Date, MoodTag, EntryType, Author, UserId)
VALUES ('Stay positive!', NOW(), 'Motivated', 'QuoteEntry', 'Unknown', 1);

INSERT INTO Entries (Content, Date, MoodTag, EntryType, Milestone, UserId)
VALUES ('Graduated college today!', NOW(), 'Happy', 'MilestoneEntry', 'Graduation', 1);

INSERT INTO Entries (Content, Date, MoodTag, EntryType, Image, UserId)
VALUES ('Trip to the beach', NOW(), 'Relaxed', 'MemoryEntry', 'beach.png', 1);

-- Insert sample entries for Bob (UserId = 2)
INSERT INTO Entries (Content, Date, MoodTag, EntryType, Author, UserId)
VALUES ('Hard work pays off.', NOW(), 'Motivated', 'QuoteEntry', 'Coach', 2);

INSERT INTO Entries (Content, Date, MoodTag, EntryType, Milestone, UserId)
VALUES ('First marathon completed!', NOW(), 'Proud', 'MilestoneEntry', 'Marathon', 2);
