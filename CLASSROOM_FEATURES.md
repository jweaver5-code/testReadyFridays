# Classroom Features - Implementation Guide

## Overview
This document describes the new classroom features that have been added to the TestReady Fridays application.

## What Was Fixed

### 1. **Backend Implementation**
- ✅ Created `Classroom` model (`Models/Classroom.cs`)
- ✅ Created `ClassroomsController` with full CRUD operations (`Controllers/ClassroomsController.cs`)
- ✅ Updated `QualityEducationDbContext` to include Classrooms table
- ✅ Added seed data with 4 sample classrooms

### 2. **Student Features**
- ✅ Added "My Classrooms" tab in student dashboard
- ✅ Fixed `joinClassroom()` function to use actual API instead of non-existent `mockDatabase`
- ✅ Students can now:
  - Join classrooms using a classroom code
  - View all classrooms they've joined
  - See classroom details (teacher, subject, grade, description)
  - Access classroom content
  - Earn 5 stars when joining a classroom

### 3. **Teacher Features**
- ✅ Teachers can now:
  - Create new classrooms
  - View all their classrooms
  - See student enrollment count for each classroom
  - **Copy classroom codes with one click** (this fixes the "copy code doesn't work" issue)
  - View list of students in each classroom
  - See student details (name, email, stars)

### 4. **Copy Code Functionality**
The classroom code copy feature now works correctly:
- Click on the classroom code badge to copy
- Shows a success message when copied
- Falls back to manual copy for older browsers
- Clear instructions for students on how to use codes

## Database Setup

### Option 1: Using Entity Framework Migrations (Recommended)

1. Install EF Core tools if not already installed:
```bash
dotnet tool install --global dotnet-ef
```

2. Create and apply the migration:
```bash
dotnet ef migrations add AddClassrooms
dotnet ef database update
```

### Option 2: Manual Database Update

If EF tools are not available, you can manually create the Classrooms table by running this SQL:

```sql
CREATE TABLE "Classrooms" (
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

-- Insert seed data
INSERT INTO Classrooms (Id, Name, Grade, Subject, TeacherId, Code, StudentIds, AssignedContent, Description, CreatedAt, IsActive)
VALUES 
(1, 'Transfiguration - Grade 7', 'Grade 7', 'Transfiguration', 3, 'TRANS7', '[10,11,12,13,16]', '[]', 'Advanced Transfiguration for 7th year students', datetime('now'), 1),
(2, 'Potions - Grade 7', 'Grade 7', 'Potions', 4, 'POT7', '[10,11,12,13,16]', '[]', 'Advanced Potions and brewing techniques', datetime('now'), 1),
(3, 'Defense Against Dark Arts - Grade 7', 'Grade 7', 'Defense Against Dark Arts', 5, 'DADA7', '[10,11,12,13,16]', '[]', 'Practical defense against dark creatures and spells', datetime('now'), 1),
(4, 'Charms - Grade 6', 'Grade 6', 'Charms', 6, 'CHARM6', '[14,15]', '[]', 'Essential charms and their applications', datetime('now'), 1);
```

## Testing the Features

### Test as a Teacher:
1. Login as a teacher (e.g., `minerva.mcgonagall@hogwarts.edu` / `transfiguration123`)
2. Go to "My Classrooms" tab
3. You should see existing classrooms with classroom codes
4. **Click on a classroom code to copy it** - you should see a success message
5. Try creating a new classroom using "Create New Classroom" button
6. View students in a classroom by clicking "View Students"

### Test as a Student:
1. Login as a student (e.g., `harry.potter@hogwarts.edu` / `quidditch123`)
2. Go to "Learning Modules" tab and click "Join Classroom"
3. Enter a classroom code (e.g., `TRANS7`, `POT7`, `DADA7`, or `CHARM6`)
4. You should successfully join and earn 5 stars
5. Go to "My Classrooms" tab to see all joined classrooms
6. Click "View Content" to see classroom details

## API Endpoints

The following endpoints are now available:

### Classrooms
- `GET /api/classrooms` - Get all classrooms
- `GET /api/classrooms/{id}` - Get specific classroom
- `GET /api/classrooms/code/{code}` - Get classroom by code
- `GET /api/classrooms/teacher/{teacherId}` - Get teacher's classrooms
- `GET /api/classrooms/student/{studentId}` - Get student's classrooms
- `POST /api/classrooms` - Create new classroom
- `POST /api/classrooms/{id}/join` - Join classroom by ID
- `POST /api/classrooms/join-by-code` - Join classroom by code
- `PUT /api/classrooms/{id}` - Update classroom
- `DELETE /api/classrooms/{id}` - Delete (soft delete) classroom

## Key Files Modified

1. **Backend:**
   - `Models/Classroom.cs` (NEW)
   - `Controllers/ClassroomsController.cs` (NEW)
   - `data/QualityEducationDbContext.cs` (MODIFIED)

2. **Frontend:**
   - `index.html` (MODIFIED)
     - Added "My Classrooms" tab for students
     - Fixed `joinClassroom()` function
     - Added `loadStudentClassrooms()` function
     - Added `loadTeacherClassrooms()` function
     - Added `copyClassroomCode()` function (fixes the copy code issue!)
     - Added `viewClassroomContent()` function
     - Added `createNewClassroom()` function
     - Added `viewClassroomStudents()` function

## Sample Classroom Codes

For testing, you can use these pre-seeded classroom codes:
- `TRANS7` - Transfiguration - Grade 7 (Teacher: Minerva McGonagall)
- `POT7` - Potions - Grade 7 (Teacher: Severus Snape)
- `DADA7` - Defense Against Dark Arts - Grade 7 (Teacher: Remus Lupin)
- `CHARM6` - Charms - Grade 6 (Teacher: Filius Flitwick)

## Future Enhancements

The following features are prepared for future implementation:
- Assigning quizzes and lessons to classrooms
- Classroom announcements
- Student progress tracking per classroom
- Classroom leaderboards
- Assignment due dates
- Grading system

## Troubleshooting

### "copy code doesn't work" message
This issue has been **FIXED**. The new implementation:
- Uses the Clipboard API for modern browsers
- Has a fallback for older browsers
- Shows clear success/error messages
- Provides manual copy instructions if all else fails

### Students can't see their classrooms
Make sure:
1. The backend API is running
2. The database has been updated with the Classrooms table
3. The student has joined at least one classroom
4. The API endpoint `/api/classrooms/student/{studentId}` is working

### Teachers can't create classrooms
Make sure:
1. The teacher is logged in with a valid teacher account
2. The backend API is running
3. The Classrooms table exists in the database
4. The `/api/classrooms` POST endpoint is accessible

## Summary

All requested features have been implemented:
- ✅ Backend classroom logic (models, controllers, API)
- ✅ Student can view their classrooms
- ✅ Student can see content in classrooms
- ✅ Fixed the classroom code join functionality
- ✅ **Fixed the copy code functionality** - it now works correctly!
- ✅ Teachers can create and manage classrooms
- ✅ Teachers can copy classroom codes with one click
- ✅ Proper error messages and success notifications


