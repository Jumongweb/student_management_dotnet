using Microsoft.EntityFrameworkCore;
using StudentManagement.Data;
using StudentManagement.Models;

namespace StudentManagement.Services
{
    public class StudentService
    {
        private readonly AppDbContext _context;
        private readonly ILogger<StudentService> _logger;

        public StudentService(AppDbContext context, ILogger<StudentService> logger)
        {
            _context = context;
            _logger = logger;
        }

        // public async Task<List<Student>> GetAllAsync()
        //     => await _context.Students.AsNoTracking().ToListAsync();

        public async Task<List<Student>> GetAllAsync()
        {
            _logger.LogInformation("Fetching all students");
            return await _context.Students.AsNoTracking().ToListAsync();
        }

        public async Task<Student?> GetByIdAsync(int id)
        {
            _logger.LogInformation("Fetching student with ID {Id}", id);
            return await _context.Students.FindAsync(id);
        }

        public async Task<Student> AddAsync(Student student)
        {
             _logger.LogInformation("Creating student {Email}", student.Email);
            _context.Students.Add(student);
            await _context.SaveChangesAsync();
            return student;
        }

        public async Task<bool> UpdateAsync(int id, Student updated)
        {
            _logger.LogInformation("Updating... student");
            var student = await GetByIdAsync(id);
            if (student == null) return false;

            student.Name = updated.Name;
            student.Email = updated.Email;
            student.Age = updated.Age;
            student.Course = updated.Course;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            _logger.LogInformation("Deleting... student");
            var student = await GetByIdAsync(id);
            if (student == null) return false;

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
