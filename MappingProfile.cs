using AutoMapper;
using StudentManagement.Models;
using StudentManagement.DTOs;

namespace StudentManagement
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Mapping from entity to DTO
            CreateMap<Student, StudentResponse>();

            // Mapping from DTO to entity
            CreateMap<StudentCreateDTO, Student>();
        }
    }
}
