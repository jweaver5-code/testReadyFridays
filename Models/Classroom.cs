using System.ComponentModel.DataAnnotations;

namespace QualityEducation.Models
{
    public class Classroom
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        
        [Required]
        [MaxLength(50)]
        public string Grade { get; set; } = string.Empty;
        
        [Required]
        [MaxLength(100)]
        public string Subject { get; set; } = string.Empty;
        
        [Required]
        public int TeacherId { get; set; }
        
        [Required]
        [MaxLength(20)]
        public string Code { get; set; } = string.Empty;
        
        public string StudentIds { get; set; } = "[]"; // JSON array of student IDs
        
        public string AssignedContent { get; set; } = "[]"; // JSON array of quiz/lesson IDs
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        public bool IsActive { get; set; } = true;
        
        [MaxLength(500)]
        public string Description { get; set; } = string.Empty;
    }
}


