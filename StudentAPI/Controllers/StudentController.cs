using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentAPI.Data;
using StudentAPI.Models;

namespace StudentAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : Controller
    {
        private readonly StudentApiDbContext dbContext;

        public StudentController(StudentApiDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStudents()
        {
            return Ok(await dbContext.students.ToListAsync());
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetStudent([FromRoute] Guid id)
        {
            var student = await dbContext.students.FindAsync(id);

            if(student == null) 
                return NotFound();

            return Ok(student);
        }
       
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddStudent (AddStudentRequest addStudentRequest)
        {
            var student = new Student()
            {
                Id = Guid.NewGuid(),
                Name = addStudentRequest.Name,
                Address = addStudentRequest.Address,
                Phone = addStudentRequest.Phone,
            };

            await dbContext.students.AddAsync(student);
            await dbContext.SaveChangesAsync();

            return Ok(student);
        }

        [HttpPut]
        [Authorize]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateStudent([FromRoute] Guid id, UpdateStudentRequest updateStudentRequest)
        {
            var student = await dbContext.students.FindAsync(id);
            if(student != null)
            {
                student.Address = updateStudentRequest.Address;
                student.Phone = updateStudentRequest.Phone;

                await dbContext.SaveChangesAsync();

                return Ok(student);
            }

            return NotFound();

        }

        [HttpDelete]
        [Authorize]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteStudent([FromRoute] Guid id)
        {
            var student = await dbContext.students.FindAsync(id);

            if(student != null)
            {
                dbContext.students.Remove(student);
                await dbContext.SaveChangesAsync();

                return Ok(student);
            }

            return NotFound();
        }
    }
}
