-- Setup script for Classrooms table
-- Run this if you haven't applied Entity Framework migrations

-- Create Classrooms table
CREATE TABLE IF NOT EXISTS "Classrooms" (
    "Id" INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
    "Name" TEXT NOT NULL,
    "Grade" TEXT NOT NULL,
    "Subject" TEXT NOT NULL,
    "TeacherId" INTEGER NOT NULL,
    "Code" TEXT NOT NULL,
    "StudentIds" TEXT NOT NULL DEFAULT '[]',
    "AssignedContent" TEXT NOT NULL DEFAULT '[]',
    "CreatedAt" TEXT NOT NULL,
    "IsActive" INTEGER NOT NULL DEFAULT 1,
    "Description" TEXT
);

-- Insert seed data for demo classrooms
INSERT INTO Classrooms (Id, Name, Grade, Subject, TeacherId, Code, StudentIds, AssignedContent, Description, CreatedAt, IsActive)
VALUES 
(1, 'Transfiguration - Grade 7', 'Grade 7', 'Transfiguration', 3, 'TRANS7', '[10,11,12,13,16]', '[]', 'Advanced Transfiguration for 7th year students', datetime('now'), 1),
(2, 'Potions - Grade 7', 'Grade 7', 'Potions', 4, 'POT7', '[10,11,12,13,16]', '[]', 'Advanced Potions and brewing techniques', datetime('now'), 1),
(3, 'Defense Against Dark Arts - Grade 7', 'Grade 7', 'Defense Against Dark Arts', 5, 'DADA7', '[10,11,12,13,16]', '[]', 'Practical defense against dark creatures and spells', datetime('now'), 1),
(4, 'Charms - Grade 6', 'Grade 6', 'Charms', 6, 'CHARM6', '[14,15]', '[]', 'Essential charms and their applications', datetime('now'), 1);

-- Verify the data
SELECT * FROM Classrooms;


