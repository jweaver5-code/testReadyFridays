# Quality Education Platform

A Khan Academy-style educational web application built with vanilla HTML, CSS, JavaScript, and Bootstrap. Students from Kindergarten to College can learn various subjects through interactive courses, quizzes, and games.

## Features

### 🎓 **Comprehensive Course Catalog**
- **8 Subject Areas**: Math, Science, English, History, Computer Science, Foreign Languages, Art & Music, and College Gen Eds
- **4 Grade Levels**: K-5 (Elementary), 6-8 (Middle School), 9-12 (High School), and College
- **Interactive Filtering**: Filter by subject, grade level, and search terms
- **Progress Tracking**: Visual progress bars for each course

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
- No additional software or installations required

### Running the Application

1. **Download/Clone** the project files to your computer
2. **Open** `index.html` in your web browser
3. **Start Learning!** Browse courses, take quizzes, and earn points

### Alternative: Local Server (Recommended)
For the best experience, serve the files through a local web server:

```bash
# Using Python 3
python -m http.server 8000

# Using Node.js (if you have it installed)
npx http-server

# Using PHP
php -S localhost:8000
```

Then open `http://localhost:8000` in your browser.

## How to Use

### 1. **Browse Courses**
- Click "Courses" in the navigation
- Use filters to find courses by subject or grade level
- Search for specific topics
- Click on any course card to view details

### 2. **Take a Course**
- Select a course to see its modules
- Click on a module to read the content
- Click "Take Quiz" to test your knowledge
- Answer questions to earn points

### 3. **Earn Points**
- Get 1 point for each correct quiz answer
- Complete modules to unlock more content
- Track your progress in the dashboard

### 4. **Play Memory Game**
- Earn 10 points to unlock the memory game
- Choose your difficulty level
- Match pairs of cards to win
- Track your moves and efficiency

### 5. **View Progress**
- Check your dashboard for statistics
- See recent activity and achievements
- Monitor progress across all subjects

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
- **HTML5**: Semantic markup and accessibility
- **CSS3**: Custom styling with animations and responsive design
- **JavaScript (ES6+)**: Modern JavaScript with classes and modules
- **Bootstrap 5.3.3**: Responsive UI framework via CDN
- **Bootstrap Icons**: Icon library for visual elements

### Data Storage
- **JSON Files**: Course content, questions, and subject data
- **localStorage**: User progress and statistics
- **No Database Required**: Fully client-side application

### Browser Compatibility
- Chrome 60+
- Firefox 55+
- Safari 12+
- Edge 79+

## File Structure

```
qualityEducation/
├── index.html              # Main HTML file
├── styles/
│   └── main.css           # Custom CSS styles
├── scripts/
│   └── main.js            # Main JavaScript application
├── data/
│   ├── subjects.json      # Subject categories
│   ├── courses.json       # Course content and modules
│   └── questions.json     # Quiz questions and answers
└── README.md              # This file
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

## Future Enhancements

Potential features for future versions:
- More subjects and courses
- Additional game types
- Badge and achievement system
- Social features and leaderboards
- Offline support with service workers
- Mobile app version
- Teacher/admin dashboard
- Custom course creation tools

## Support

If you encounter any issues or have suggestions for improvement, please check the browser console for error messages and ensure all files are properly loaded.

## License

This project is open source and available under the MIT License.

---

**Happy Learning!** 🎓✨

