using Microsoft.EntityFrameworkCore;
using QualityEducation.Data;
using QualityEducation.Models;

// Seed initial data method
static async Task SeedInitialData(QualityEducationDbContext context)
{
    // Admin user
    var adminUser = new User
    {
        Id = 1,
        FirstName = "Admin",
        LastName = "User",
        Email = "admin@qualityeducation.com",
        Password = "admin123",
        Role = "admin",
        Grade = "N/A",
        Stars = 0,
        CompletedModules = "[]",
        CompletedQuizzes = "[]",
        GamesPlayed = 0,
        RecentActivity = "[]",
        CreatedAt = DateTime.UtcNow,
        LastLogin = DateTime.UtcNow,
        IsActive = true
    };

    // Harry Potter Teachers
    var teachers = new[]
    {
        new { FirstName = "Albus", LastName = "Dumbledore", Email = "albus.dumbledore@hogwarts.edu", Password = "phoenix123", Grade = "Multiple Grades" },
        new { FirstName = "Minerva", LastName = "McGonagall", Email = "minerva.mcgonagall@hogwarts.edu", Password = "transfiguration123", Grade = "Multiple Grades" },
        new { FirstName = "Severus", LastName = "Snape", Email = "severus.snape@hogwarts.edu", Password = "potions123", Grade = "Multiple Grades" },
        new { FirstName = "Remus", LastName = "Lupin", Email = "remus.lupin@hogwarts.edu", Password = "defense123", Grade = "Multiple Grades" }
    };

    // Harry Potter Students
    var students = new[]
    {
        new { FirstName = "Harry", LastName = "Potter", Email = "harry.potter@hogwarts.edu", Password = "quidditch123", Grade = "Grade 7", Stars = 100 },
        new { FirstName = "Hermione", LastName = "Granger", Email = "hermione.granger@hogwarts.edu", Password = "books123", Grade = "Grade 7", Stars = 0 },
        new { FirstName = "Ron", LastName = "Weasley", Email = "ron.weasley@hogwarts.edu", Password = "chess123", Grade = "Grade 7", Stars = 0 },
        new { FirstName = "Neville", LastName = "Longbottom", Email = "neville.longbottom@hogwarts.edu", Password = "herbology123", Grade = "Grade 7", Stars = 0 },
        new { FirstName = "Luna", LastName = "Lovegood", Email = "luna.lovegood@hogwarts.edu", Password = "nargles123", Grade = "Grade 6", Stars = 0 },
        new { FirstName = "Ginny", LastName = "Weasley", Email = "ginny.weasley@hogwarts.edu", Password = "batbogey123", Grade = "Grade 6", Stars = 0 },
        new { FirstName = "Draco", LastName = "Malfoy", Email = "draco.malfoy@hogwarts.edu", Password = "slytherin123", Grade = "Grade 7", Stars = 0 },
        new { FirstName = "Cedric", LastName = "Diggory", Email = "cedric.diggory@hogwarts.edu", Password = "hufflepuff123", Grade = "Grade 7", Stars = 0 }
    };

    // Add admin user
    context.Users.Add(adminUser);

    // Add teachers
    for (int i = 0; i < teachers.Length; i++)
    {
        context.Users.Add(new User
        {
            Id = i + 2,
            FirstName = teachers[i].FirstName,
            LastName = teachers[i].LastName,
            Email = teachers[i].Email,
            Password = teachers[i].Password,
            Role = "teacher",
            Grade = teachers[i].Grade,
            Stars = 0,
            CompletedModules = "[]",
            CompletedQuizzes = "[]",
            GamesPlayed = 0,
            RecentActivity = "[]",
            CreatedAt = DateTime.UtcNow,
            LastLogin = DateTime.UtcNow,
            IsActive = true
        });
    }

    // Add students
    for (int i = 0; i < students.Length; i++)
    {
        context.Users.Add(new User
        {
            Id = i + 10,
            FirstName = students[i].FirstName,
            LastName = students[i].LastName,
            Email = students[i].Email,
            Password = students[i].Password,
            Role = "student",
            Grade = students[i].Grade,
            Stars = students[i].Stars,
            CompletedModules = "[]",
            CompletedQuizzes = "[]",
            GamesPlayed = 0,
            RecentActivity = "[]",
            CreatedAt = DateTime.UtcNow,
            LastLogin = DateTime.UtcNow,
            IsActive = true
        });
    }

    await context.SaveChangesAsync();
    Console.WriteLine("Initial data seeded successfully.");
}

static async Task SeedClassroomData(QualityEducationDbContext context)
{
    try
    {
        // Check if classrooms already exist
        if (await context.Classrooms.AnyAsync())
        {
            Console.WriteLine("Classrooms already seeded.");
            return;
        }
    }
    catch
    {
        Console.WriteLine("Classrooms table doesn't exist yet, will skip seeding for now.");
        Console.WriteLine("Please run: dotnet ef database update or restart the application.");
        return;
    }

    // Get teacher IDs
    var teachers = await context.Users.Where(u => u.Role == "teacher").ToListAsync();
    var students = await context.Users.Where(u => u.Role == "student").ToListAsync();
    
    if (teachers.Count < 4 || students.Count < 5)
    {
        Console.WriteLine("Not enough teachers or students to seed classrooms.");
        return;
    }

    var classrooms = new[]
    {
        new Classroom
        {
            Name = "Transfiguration - Grade 7",
            Grade = "Grade 7",
            Subject = "Transfiguration",
            TeacherId = teachers[1].Id, // Minerva McGonagall
            Code = "TRANS7",
            StudentIds = System.Text.Json.JsonSerializer.Serialize(new List<int> { students[0].Id, students[1].Id, students[2].Id, students[3].Id, students[6].Id }),
            AssignedContent = "[]",
            Description = "Advanced Transfiguration for 7th year students",
            CreatedAt = DateTime.UtcNow,
            IsActive = true
        },
        new Classroom
        {
            Name = "Potions - Grade 7",
            Grade = "Grade 7",
            Subject = "Potions",
            TeacherId = teachers[2].Id, // Severus Snape
            Code = "POT7",
            StudentIds = System.Text.Json.JsonSerializer.Serialize(new List<int> { students[0].Id, students[1].Id, students[2].Id, students[3].Id, students[6].Id }),
            AssignedContent = "[]",
            Description = "Advanced Potions and brewing techniques",
            CreatedAt = DateTime.UtcNow,
            IsActive = true
        },
        new Classroom
        {
            Name = "Defense Against Dark Arts - Grade 7",
            Grade = "Grade 7",
            Subject = "Defense Against Dark Arts",
            TeacherId = teachers[3].Id, // Remus Lupin
            Code = "DADA7",
            StudentIds = System.Text.Json.JsonSerializer.Serialize(new List<int> { students[0].Id, students[1].Id, students[2].Id, students[3].Id, students[6].Id }),
            AssignedContent = "[]",
            Description = "Practical defense against dark creatures and spells",
            CreatedAt = DateTime.UtcNow,
            IsActive = true
        },
        new Classroom
        {
            Name = "Charms - Grade 6",
            Grade = "Grade 6",
            Subject = "Charms",
            TeacherId = teachers[0].Id, // Albus Dumbledore
            Code = "CHARM6",
            StudentIds = System.Text.Json.JsonSerializer.Serialize(new List<int> { students[4].Id, students[5].Id }),
            AssignedContent = "[]",
            Description = "Essential charms and their applications",
            CreatedAt = DateTime.UtcNow,
            IsActive = true
        }
    };

    foreach (var classroom in classrooms)
    {
        context.Classrooms.Add(classroom);
    }

    await context.SaveChangesAsync();
    Console.WriteLine("Classroom data seeded successfully.");
}

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add Entity Framework
builder.Services.AddDbContext<QualityEducationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection") ?? 
    "Data Source=data/qualityEducation_new.db"));

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.SetIsOriginAllowed(origin => true)
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials();
    });
});

var app = builder.Build();

// Configure the application to run on port 3000
app.Urls.Add("http://localhost:3000");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");

// Serve static files (HTML, CSS, JS) from the root directory
app.UseDefaultFiles();
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new Microsoft.Extensions.FileProviders.PhysicalFileProvider(
        Path.Combine(Directory.GetCurrentDirectory())),
    RequestPath = ""
});

app.UseAuthorization();

app.MapControllers();

// Ensure database is created and seeded
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<QualityEducationDbContext>();
    
    // Check if Classrooms table exists
    bool classroomsTableExists = false;
    try
    {
        var classroomCount = await context.Classrooms.CountAsync();
        classroomsTableExists = true;
        Console.WriteLine($"Classrooms table exists with {classroomCount} entries.");
    }
    catch
    {
        Console.WriteLine("Classrooms table does not exist, will be created.");
    }
    
    // Check if database exists
    var exists = context.Database.CanConnect();
    if (!exists)
    {
        Console.WriteLine("Database does not exist, creating with seed data...");
        context.Database.EnsureCreated();
        
        // Seed initial data only when database is first created
        await SeedInitialData(context);
        await SeedClassroomData(context);
    }
    else if (!classroomsTableExists)
    {
        Console.WriteLine("Database exists but Classrooms table is missing, applying migrations...");
        try
        {
            await context.Database.MigrateAsync();
            Console.WriteLine("Migrations applied successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error applying migrations: {ex.Message}");
            Console.WriteLine("Trying alternative method...");
            context.Database.EnsureCreated();
        }
        
        // Only seed data if Users table is empty
        var userCount = await context.Users.CountAsync();
        if (userCount == 0)
        {
            await SeedInitialData(context);
        }
        else
        {
            Console.WriteLine("Users table has data, skipping seed.");
        }
        
        // Now that tables are created, seed classroom data
        Console.WriteLine("Seeding classroom data...");
        await SeedClassroomData(context);
    }
    else
    {
        Console.WriteLine("Database already exists, using existing data.");
        
        // Check if classrooms need to be seeded
        var classroomCount = await context.Classrooms.CountAsync();
        if (classroomCount == 0)
        {
            Console.WriteLine("No classrooms found, seeding initial classroom data...");
            await SeedClassroomData(context);
        }
        
        // Check Harry Potter's current star count
        var harryPotter = await context.Users.FirstOrDefaultAsync(u => u.FirstName == "Harry" && u.LastName == "Potter");
        if (harryPotter != null)
        {
            Console.WriteLine($"Harry Potter's current star count: {harryPotter.Stars}");
            Console.WriteLine($"Harry Potter's ID: {harryPotter.Id}");
        }
        else
        {
            Console.WriteLine("Harry Potter not found in database!");
        }
    }
}

app.Run();
