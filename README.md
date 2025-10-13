# Quality Education Platform

A Khan Academy-style educational web application built with vanilla HTML, CSS, JavaScript, and Bootstrap. Students from Kindergarten to College can learn various subjects through interactive courses, quizzes, and games.

## Features

### 🏫 **Classroom Management** ⭐ NEW!
- **For Teachers**:
  - Create and manage multiple classrooms
  - Generate unique classroom codes automatically
  - **One-click code copying** to share with students
  - View enrolled students and their progress
  - Track student engagement and stars earned
- **For Students**:
  - Join classrooms using teacher-provided codes
  - View all joined classrooms in "My Classrooms" tab
  - See classroom details (teacher, subject, grade)
  - Access classroom-specific content
  - Earn 5 stars when joining a classroom

### 📚 **Learning Modules**
- **Structured Content**: Each course contains multiple learning modules
- **Rich Content**: Detailed explanations and examples for each topic
- **Module Progression**: Complete modules in order to build knowledge

### 🧠 **Quiz System**
- **Interactive Quizzes**: Multiple-choice questions for each module
- **Points System**: Earn 1 point for each correct answer
- **Immediate Feedback**: See correct answers and explanations
- **Progress Tracking**: Track quiz scores and completion

### 🎮 **Memory Matching Game**
- **Unlock Requirement**: Earn 10 points to unlock the game
- **3 Difficulty Levels**:
  - **Easy (K-5)**: 4x4 grid, 8 pairs
  - **Medium (6-12)**: 6x6 grid, 18 pairs  
  - **Hard (College)**: 8x8 grid, 32 pairs
- **Move Counter**: Track your efficiency
- **Visual Feedback**: Smooth animations and effects

### 📊 **User Dashboard**
- **Statistics**: Total points, quizzes passed, modules completed, games played
- **Recent Activity**: Track your learning progress
- **Subject Progress**: See completion rates for each subject area
- **Achievement Tracking**: Monitor your educational journey

## Getting Started

### Prerequisites
- A modern web browser (Chrome, Firefox, Safari, Edge)
- .NET 8.0 SDK (for backend API)
- SQLite (database)

### Running the Application

#### Backend Setup
1. **Update Database**: Run the classroom setup script
   ```bash
   sqlite3 data/qualityEducation_new.db < setup_classrooms.sql
   ```

2. **Build and Run**: Start the .NET backend
   ```bash
   dotnet build
   dotnet run
   ```
   The API will start on `http://localhost:3000`

#### Frontend
1. **Open** `index.html` in your web browser
2. **Login** with demo credentials (see below)
3. **Start Learning!** Browse courses, join classrooms, and earn points

### Demo Credentials

**Teachers**:
- Email: `minerva.mcgonagall@hogwarts.edu`, Password: `transfiguration123`
- Email: `severus.snape@hogwarts.edu`, Password: `potions123`

**Students**:
- Email: `harry.potter@hogwarts.edu`, Password: `quidditch123`
- Email: `hermione.granger@hogwarts.edu`, Password: `books123`

**Admin**:
- Email: `admin@qualityeducation.com`, Password: `admin123`

### Classroom Codes (for testing)
- `TRANS7` - Transfiguration (Grade 7)
- `POT7` - Potions (Grade 7)
- `DADA7` - Defense Against Dark Arts (Grade 7)
- `CHARM6` - Charms (Grade 6)

## How to Use

### For Teachers 👨‍🏫

#### 1. **Create a Classroom**
- Login as a teacher
- Go to "My Classrooms" tab
- Click "Create New Classroom"
- Fill in classroom details (name, grade, subject)
- Get your unique classroom code!

#### 2. **Share Classroom Code**
- **Click the classroom code badge** to copy it instantly
- You'll see a success message when copied
- Share the code with your students
- Watch your classroom grow!

#### 3. **Manage Students**
- Click "View Students" to see enrolled students
- Monitor student progress and stars earned
- Track engagement in your classroom

### For Students 🎓

#### 1. **Join a Classroom**
- Login as a student
- Go to "Learning Modules" tab
- Click "Join Classroom"
- Enter the code from your teacher
- Earn 5 stars for joining!

#### 2. **View Your Classrooms**
- Go to "My Classrooms" tab
- See all classrooms you've joined
- View classroom details and teacher info
- Click "View Content" to access materials

#### 3. **Complete Learning Modules**
- Browse available learning modules
- Complete modules to earn stars
- Take quizzes to test your knowledge
- Track your progress

#### 4. **Play Games**
- Spend stars to unlock games
- Choose from Flappy Bird, Snake, or Memory Game
- Have fun while learning!

#### 5. **View Progress**
- Check your dashboard for statistics
- See total stars earned
- Monitor modules completed

## Course Content

### Mathematics
- **K-5**: Basic numbers, addition, subtraction
- **6-8**: Fractions, decimals, pre-algebra, geometry
- **9-12**: Calculus, trigonometry, advanced algebra
- **College**: Statistics, linear algebra

### Science
- **K-5**: Nature science, plants and animals
- **6-8**: Biology, cell structure, genetics
- **9-12**: Chemistry, physics, advanced biology
- **College**: Advanced chemistry, physics, environmental science

### English Language Arts
- **K-5**: Phonics, sight words, basic reading
- **6-8**: Grammar, writing, literature basics
- **9-12**: Literature analysis, advanced writing
- **College**: Critical thinking, advanced literature

### History & Social Studies
- **6-8**: World history, ancient civilizations
- **9-12**: US history, government, economics
- **College**: Advanced history, political science

### Computer Science
- **6-8**: Computer basics, visual programming
- **9-12**: Python programming, web development
- **College**: Advanced programming, algorithms

### College General Education
- **Psychology**: Introduction to human behavior
- **Sociology**: Society and social institutions
- **Philosophy**: Critical thinking and ethics
- **And more...**

## Technical Details

### Built With
- **Frontend**:
  - HTML5: Semantic markup and accessibility
  - CSS3: Custom styling with animations and responsive design
  - JavaScript (ES6+): Modern JavaScript with async/await
  - Bootstrap 5.3.3: Responsive UI framework
  - Font Awesome 6.0: Icon library

- **Backend**:
  - ASP.NET Core 8.0: REST API
  - Entity Framework Core: ORM for database
  - SQLite: Lightweight database
  - C# 12: Modern language features

### Data Storage
- **SQLite Database**: Users, Classrooms, progress data
- **localStorage**: Client-side caching and session data
- **JSON**: Course content and quiz questions

### Browser Compatibility
- Chrome 60+
- Firefox 55+
- Safari 12+
- Edge 79+

## File Structure

```
testReadyFridays/
├── index.html                          # Main frontend application
├── Models/
│   ├── User.cs                        # User data model
│   └── Classroom.cs                   # Classroom data model ⭐ NEW
├── Controllers/
│   ├── UsersController.cs             # User API endpoints
│   └── ClassroomsController.cs        # Classroom API endpoints ⭐ NEW
├── data/
│   ├── QualityEducationDbContext.cs   # Database context
│   ├── qualityEducation_new.db       # SQLite database
│   ├── courses.json                   # Course content
│   ├── questions.json                 # Quiz questions
│   └── subjects.json                  # Subject categories
├── setup_classrooms.sql               # Database setup script ⭐ NEW
├── QUICK_START_CLASSROOMS.md         # Testing guide ⭐ NEW
├── CLASSROOM_FEATURES.md             # Technical docs ⭐ NEW
├── IMPLEMENTATION_SUMMARY.md         # Implementation details ⭐ NEW
└── README.md                          # This file
```

## Customization

### Adding New Courses
1. Edit `data/courses.json` to add new courses
2. Add corresponding questions to `data/questions.json`
3. The application will automatically load new content

### Modifying Questions
1. Edit `data/questions.json`
2. Questions are organized by module ID
3. Each question needs: question text, options, correct answer index, and explanation

### Styling Changes
1. Edit `styles/main.css` for visual modifications
2. CSS variables are defined at the top for easy color changes
3. Bootstrap classes can be customized or overridden

## Troubleshooting

### Common Issues

**Courses not loading?**
- Make sure all JSON files are in the `data/` folder
- Check browser console for JavaScript errors
- Ensure you're serving files through a web server

**Progress not saving?**
- Check if localStorage is enabled in your browser
- Clear browser cache and try again
- Some browsers block localStorage in file:// protocol

**Memory game not working?**
- Ensure you have earned at least 10 points
- Check browser console for errors
- Try refreshing the page

### Browser Console
Press F12 to open developer tools and check the Console tab for any error messages.

## Recent Updates ⭐

### Version 2.0 - Classroom Features (Latest)
- ✅ Full classroom management system
- ✅ Teachers can create and manage classrooms
- ✅ **One-click classroom code copying**
- ✅ Students can join classrooms
- ✅ "My Classrooms" view for students
- ✅ Student enrollment tracking
- ✅ Star rewards for participation

### Quick Start
See `QUICK_START_CLASSROOMS.md` for detailed testing instructions.

## Future Enhancements

Potential features for future versions:
- Assign quizzes and lessons to classrooms
- Classroom announcements and messaging
- Advanced grading and feedback system
- Classroom leaderboards
- More subjects and courses
- Additional game types
- Badge and achievement system
- Offline support with service workers
- Mobile app version

## Support

If you encounter any issues or have suggestions for improvement, please check the browser console for error messages and ensure all files are properly loaded.

## License

This project is open source and available under the MIT License.

---

**Happy Learning!** 🎓✨

