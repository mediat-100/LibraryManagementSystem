using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Application.Dtos.Responses
{
    public class ErrorResponse
    {
        public string Message { get; set; }
        public string? StackTrace { get; set; }
        public string? InnerException { get; set; }
    }
}
