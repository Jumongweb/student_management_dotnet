using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagement.DTOs
{
    public class StudentUpdateDTO
    {
        public string Name { get; set; } = string.Empty;
        [Range(1, 100)]
        public int Age { get; set; }
        [Required]
        public string Course { get; set; } = string.Empty;
    }
}