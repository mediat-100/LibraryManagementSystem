using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Application.Dtos.Requests
{

    public class AddUserRequestDto
    {
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class UpdateUserRequestDto
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
    }
}
