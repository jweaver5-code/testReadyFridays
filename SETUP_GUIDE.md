# Quality Education Setup Guide

Everything is now in the main `qualityEducation` folder - no separate API folder needed!

## Project Structure

```
qualityEducation/
├── Program.cs                    # C# API startup
├── appsettings.json             # Database configuration
├── QualityEducation.csproj      # Project file
├── Models/
│   └── User.cs                  # User model
├── Data/
│   └── QualityEducationDbContext.cs  # Database context
├── Controllers/
│   └── UsersController.cs       # API endpoints
├── data/
│   └── qualityEducation.db      # SQLite database (auto-created)
├── index.html                   # Original frontend
├── api-frontend.html           # API-based frontend
└── populate-database.html      # Database population tool
```

## Quick Setup

### Step 1: Install .NET 8 SDK
Download from https://dotnet.microsoft.com/download

### Step 2: Install Dependencies
```bash
dotnet restore
```

### Step 3: Run the API
```bash
dotnet run
```
The API will start on `http://localhost:5000`

### Step 4: Open Frontend
- **Original**: Open `index.html` in browser
- **API Version**: Open `api-frontend.html` in browser

## Database

The database (`data/qualityEducation.db`) will be created automatically with:
- **1 Admin user** (admin@qualityeducation.com / admin123)
- **10 Harry Potter teachers** (all professors)
- **15 Harry Potter students** (main characters)

## Test Accounts

- **Admin**: admin@qualityeducation.com / admin123
- **Teacher**: albus.dumbledore@hogwarts.edu / phoenix123
- **Student**: harry.potter@hogwarts.edu / quidditch123

## API Endpoints

- `GET /api/users` - Get all users
- `POST /api/users/login` - User login
- `GET /api/users/{id}` - Get specific user

## Troubleshooting

### Database Issues
If the database doesn't populate automatically:
1. Open `populate-database.html`
2. Click "Populate Database"
3. Replace your `data/qualityEducation.db` file

### CORS Issues
The API is configured to allow requests from localhost ports 3000, 5500, and 8080.

### Port Issues
If port 5000 is busy, the API will use the next available port. Check the console output for the actual port.

## Development

### Adding New Features
1. Add models in `Models/`
2. Update `Data/QualityEducationDbContext.cs` if needed
3. Create controllers in `Controllers/`
4. Update frontend API calls

### Database Changes
```bash
dotnet ef migrations add MigrationName
dotnet ef database update
```

Everything is now in one folder - no separate API directory needed!

