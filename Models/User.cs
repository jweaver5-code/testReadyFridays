using System.ComponentModel.DataAnnotations;

namespace QualityEducation.Models
{
    public class User
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string Email { get; set; } = string.Empty;
        
        [Required]
        [MaxLength(100)]
        public string Password { get; set; } = string.Empty;
        
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; } = string.Empty;
        
        [Required]
        [MaxLength(50)]
        public string LastName { get; set; } = string.Empty;
        
        [Required]
        [MaxLength(20)]
        public string Grade { get; set; } = string.Empty;
        
        [Required]
        [MaxLength(20)]
        public string Role { get; set; } = string.Empty;
        
        public int Stars { get; set; } = 0;
        
        public string CompletedModules { get; set; } = "[]";
        
        public string CompletedQuizzes { get; set; } = "[]";
        
        public int GamesPlayed { get; set; } = 0;
        
        public string RecentActivity { get; set; } = "[]";
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        public DateTime LastLogin { get; set; } = DateTime.UtcNow;
        
        public bool IsActive { get; set; } = true;
    }
}

