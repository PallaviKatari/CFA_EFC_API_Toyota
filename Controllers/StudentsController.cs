using CFA_EFC_API.Data;
using CFA_EFC_API.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace CFA_EFC_API.Controllers
{
    [ApiController]
    [Route("api/students")]
    public class StudentsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public StudentsController(AppDbContext context)
        {
            _context = context;
        }

        // READ
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var students = await _context
                .Set<StudentDto>()
                .FromSqlRaw("EXEC dbo.GetStudents") // Call the procedure
                .AsNoTracking() 
                .ToListAsync();

            return Ok(students);
        }

        // WRITE
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] StudentDto model)
        {
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC dbo.InsertStudent @Name, @Age",
                new SqlParameter("@Name", model.Name),
                new SqlParameter("@Age", model.Age)
            );

            return Ok("Student inserted");
        }
    }
}
