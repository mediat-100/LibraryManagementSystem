using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Application.Dtos.Responses
{
    public class AuthResponseDto
    {
        public long UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string AccessToken { get; set; }
        public DateTime AccessTokenExpiresAt { get; set; }
        public string RefreshToken { get; set; }
    }
}
