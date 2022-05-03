using HealthAPI.Enums;
using System;

namespace HealthAPI.Dtos
{
    public record PatientDto(Guid Id, string UserId, string FirstName,
        string LastName, Gender Gender, DateTime DateOfBirth,
        string Address, DateTime CreatedAt, DateTime UpdatedAt);
}
