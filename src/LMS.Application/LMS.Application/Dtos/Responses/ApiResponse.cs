using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Application.Dtos.Responses
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T? Data { get; set; }
        public ErrorResponse? Error { get; set; }
        public DateTime Timestamp { get; set; }

        public static ApiResponse<T> Ok(T? data, string message = "Request successful")
        {
            return new ApiResponse<T>
            {
                Success = true,
                Message = message,
                Data = data,
                Timestamp = DateTime.UtcNow
            };
        }

        public static ApiResponse<T> Fail(string message, ErrorResponse? error = null)
        {
            return new ApiResponse<T>
            {
                Success = false,
                Message = message,
                Error = error,
                Timestamp = DateTime.UtcNow
            };
        }
    }
}