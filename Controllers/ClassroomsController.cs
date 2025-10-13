using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QualityEducation.Data;
using QualityEducation.Models;
using System.Text.Json;

namespace QualityEducation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClassroomsController : ControllerBase
    {
        private readonly QualityEducationDbContext _context;

        public ClassroomsController(QualityEducationDbContext context)
        {
            _context = context;
        }

        // GET: api/classrooms
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Classroom>>> GetClassrooms()
        {
            return await _context.Classrooms.ToListAsync();
        }

        // GET: api/classrooms/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Classroom>> GetClassroom(int id)
        {
            var classroom = await _context.Classrooms.FindAsync(id);

            if (classroom == null)
            {
                return NotFound();
            }

            return classroom;
        }

        // GET: api/classrooms/code/{code}
        [HttpGet("code/{code}")]
        public async Task<ActionResult<Classroom>> GetClassroomByCode(string code)
        {
            var classroom = await _context.Classrooms
                .FirstOrDefaultAsync(c => c.Code == code.ToUpper());

            if (classroom == null)
            {
                return NotFound(new { message = "Invalid classroom code" });
            }

            return classroom;
        }

        // GET: api/classrooms/teacher/{teacherId}
        [HttpGet("teacher/{teacherId}")]
        public async Task<ActionResult<IEnumerable<Classroom>>> GetTeacherClassrooms(int teacherId)
        {
            var classrooms = await _context.Classrooms
                .Where(c => c.TeacherId == teacherId && c.IsActive)
                .ToListAsync();

            return classrooms;
        }

        // GET: api/classrooms/student/{studentId}
        [HttpGet("student/{studentId}")]
        public async Task<ActionResult<IEnumerable<Classroom>>> GetStudentClassrooms(int studentId)
        {
            var allClassrooms = await _context.Classrooms
                .Where(c => c.IsActive)
                .ToListAsync();

            var studentClassrooms = allClassrooms
                .Where(c => 
                {
                    try
                    {
                        var studentIds = JsonSerializer.Deserialize<List<int>>(c.StudentIds);
                        return studentIds != null && studentIds.Contains(studentId);
                    }
                    catch
                    {
                        return false;
                    }
                })
                .ToList();

            return studentClassrooms;
        }

        // POST: api/classrooms
        [HttpPost]
        public async Task<ActionResult<Classroom>> CreateClassroom(Classroom classroom)
        {
            // Generate unique classroom code
            classroom.Code = GenerateUniqueCode();
            classroom.CreatedAt = DateTime.UtcNow;
            classroom.IsActive = true;

            _context.Classrooms.Add(classroom);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetClassroom), new { id = classroom.Id }, classroom);
        }

        // POST: api/classrooms/{id}/join
        [HttpPost("{id}/join")]
        public async Task<ActionResult> JoinClassroom(int id, [FromBody] JoinClassroomRequest request)
        {
            var classroom = await _context.Classrooms.FindAsync(id);
            if (classroom == null)
            {
                return NotFound();
            }

            try
            {
                var studentIds = JsonSerializer.Deserialize<List<int>>(classroom.StudentIds) ?? new List<int>();
                
                if (!studentIds.Contains(request.StudentId))
                {
                    studentIds.Add(request.StudentId);
                    classroom.StudentIds = JsonSerializer.Serialize(studentIds);
                    await _context.SaveChangesAsync();
                }

                return Ok(new { message = "Successfully joined classroom", classroom });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Error joining classroom", error = ex.Message });
            }
        }

        // POST: api/classrooms/join-by-code
        [HttpPost("join-by-code")]
        public async Task<ActionResult> JoinClassroomByCode([FromBody] JoinByCodeRequest request)
        {
            var classroom = await _context.Classrooms
                .FirstOrDefaultAsync(c => c.Code == request.Code.ToUpper() && c.IsActive);

            if (classroom == null)
            {
                return NotFound(new { message = "Invalid classroom code" });
            }

            try
            {
                var studentIds = JsonSerializer.Deserialize<List<int>>(classroom.StudentIds) ?? new List<int>();
                
                if (!studentIds.Contains(request.StudentId))
                {
                    studentIds.Add(request.StudentId);
                    classroom.StudentIds = JsonSerializer.Serialize(studentIds);
                    await _context.SaveChangesAsync();
                }

                return Ok(new { message = "Successfully joined classroom", classroom });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Error joining classroom", error = ex.Message });
            }
        }

        // PUT: api/classrooms/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateClassroom(int id, Classroom classroom)
        {
            if (id != classroom.Id)
            {
                return BadRequest();
            }

            _context.Entry(classroom).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClassroomExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/classrooms/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClassroom(int id)
        {
            var classroom = await _context.Classrooms.FindAsync(id);
            if (classroom == null)
            {
                return NotFound();
            }

            // Soft delete
            classroom.IsActive = false;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ClassroomExists(int id)
        {
            return _context.Classrooms.Any(e => e.Id == id);
        }

        private string GenerateUniqueCode()
        {
            string code;
            do
            {
                code = GenerateRandomCode();
            }
            while (_context.Classrooms.Any(c => c.Code == code));

            return code;
        }

        private string GenerateRandomCode()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, 6)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }

    public class JoinClassroomRequest
    {
        public int StudentId { get; set; }
    }

    public class JoinByCodeRequest
    {
        public string Code { get; set; } = string.Empty;
        public int StudentId { get; set; }
    }
}


