using System;

namespace HealthAPI.Dtos
{
    public class HealthReportResponseDto
    {
        public Boolean Status { get; }
        public TimeSpan TotalDuration { get; }
    }
}
