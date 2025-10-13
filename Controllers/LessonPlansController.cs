using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace QualityEducation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LessonPlansController : ControllerBase
    {
        private readonly IWebHostEnvironment _environment;

        public LessonPlansController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        [HttpGet]
        public async Task<IActionResult> GetLessonPlans()
        {
            try
            {
                var filePath = Path.Combine(_environment.ContentRootPath, "data", "lesson-plans.json");
                if (!System.IO.File.Exists(filePath))
                {
                    return NotFound("Lesson plans file not found");
                }

                var jsonContent = await System.IO.File.ReadAllTextAsync(filePath);
                var lessonPlans = JsonSerializer.Deserialize<object>(jsonContent);
                return Ok(lessonPlans);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error loading lesson plans: {ex.Message}");
            }
        }

        [HttpGet("test-prep-guide")]
        public async Task<IActionResult> GetTestPrepGuide()
        {
            try
            {
                var filePath = Path.Combine(_environment.ContentRootPath, "data", "test-prep-guide.json");
                if (!System.IO.File.Exists(filePath))
                {
                    return NotFound("Test prep guide file not found");
                }

                var jsonContent = await System.IO.File.ReadAllTextAsync(filePath);
                var testPrepGuide = JsonSerializer.Deserialize<object>(jsonContent);
                return Ok(testPrepGuide);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error loading test prep guide: {ex.Message}");
            }
        }

        [HttpGet("practice-tests")]
        public async Task<IActionResult> GetPracticeTests()
        {
            try
            {
                var filePath = Path.Combine(_environment.ContentRootPath, "data", "practice-tests.json");
                if (!System.IO.File.Exists(filePath))
                {
                    return NotFound("Practice tests file not found");
                }

                var jsonContent = await System.IO.File.ReadAllTextAsync(filePath);
                var practiceTests = JsonSerializer.Deserialize<object>(jsonContent);
                return Ok(practiceTests);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error loading practice tests: {ex.Message}");
            }
        }
    }
}







