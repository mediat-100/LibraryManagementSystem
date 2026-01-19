using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMS.Domain.Enums;

namespace LMS.Application.Dtos.Responses
{
    public class UserResponseDto
    {
        public long Id { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public Role Role { get; set; }
        public RecordStatus RecordStatus { get; set; }
        public string RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
    }
}
