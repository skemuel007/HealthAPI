using System;
using System.ComponentModel.DataAnnotations;

namespace HealthAPI.Models
{
    public class BaseEntity
    {
        [Key]
        public Guid Id { get; set; }
    }
}
