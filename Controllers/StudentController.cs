using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using StudentManagement.DTOs;
using StudentManagement.Models;
using StudentManagement.Services;

namespace StudentManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly StudentService _service;
        private readonly IMapper _mapper;

        public StudentController(StudentService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] StudentCreateDTO createDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ApiResponse<object>
                {
                    Status = false,
                    StatusCode = 400,
                    Message = "Validation errors",
                    Data = ModelState
                });

            var student = _mapper.Map<Student>(createDto);
            var created = await _service.AddAsync(student);

            var readDto = _mapper.Map<StudentResponse>(created);

            var response = new ApiResponse<StudentResponse>
            {
                Status = true,
                StatusCode = 201,
                Message = "Student created successfully",
                Data = readDto
            };

            return CreatedAtAction(nameof(GetById), new { id = readDto.Id }, response);
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var students = await _service.GetAllAsync();
            var dto = _mapper.Map<List<StudentResponse>>(students);

            var response = new ApiResponse<List<StudentResponse>>
            {
                Status = true,
                StatusCode = 200,
                Message = "Students retrieved successfully",
                Data = dto
            };

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var student = await _service.GetByIdAsync(id);
            if (student == null)
            {
                return NotFound(new ApiResponse<string>
                {
                    Status = false,
                    StatusCode = 404,
                    Message = "Student not found",
                    Data = null
                });
            }

            var dto = _mapper.Map<StudentResponse>(student);
            var response = new ApiResponse<StudentResponse>
            {
                Status = true,
                StatusCode = 200,
                Message = "Student retrieved successfully",
                Data = dto
            };

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _service.DeleteAsync(id);

            if (!success)
            {
                // Return 404 if the student was not found
                var notFoundResponse = new ApiResponse<string>
                {
                    Status = false,
                    StatusCode = 404,
                    Message = $"Student with ID {id} not found",
                    Data = null
                };
                return NotFound(notFoundResponse);
            }

            // Return success response
            var response = new ApiResponse<string>
            {
                Status = true,
                StatusCode = 200,
                Message = $"Student with ID {id} deleted successfully",
                Data = null
            };

            return Ok(response);
        }


    }
}
