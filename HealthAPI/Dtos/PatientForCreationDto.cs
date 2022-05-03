﻿using HealthAPI.Enums;
using System;

namespace HealthAPI.Dtos
{
    public record PatientForCreationDto(string UserId, string FirstName,
        string Address,
       string LastName, Gender Gender, DateTime DateOfBirth);
}
