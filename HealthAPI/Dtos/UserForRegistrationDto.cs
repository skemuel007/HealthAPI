using HealthAPI.Enums;
using System.Collections.Generic;

namespace HealthAPI.Dtos
{
    public class UserForAuthenticationDto
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
    }
    public class UserForRegistrationDto
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public ICollection<string> Roles { get; init; }
    }
}
