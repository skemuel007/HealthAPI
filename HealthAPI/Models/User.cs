using HealthAPI.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Text.Json.Serialization;

namespace HealthAPI.Models
{
    public class User : IdentityUser { 
        public string FirstName { get; set; }
        public string LastName { get; set; }  
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public virtual Patient Patient { get; set; }
    }
}
