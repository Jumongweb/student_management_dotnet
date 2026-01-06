using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagement.DTOs
{
    public class ApiResponse<T>
    {
        public string ReferenceCode { get; set; } = Guid.NewGuid().ToString();
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public bool Status { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; } = string.Empty;
        public T Data { get; set; } = default!;
        public object? Metadata { get; set; } = null;
    }

}