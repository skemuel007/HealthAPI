using System;

namespace HealthAPI.Utils
{
    public class NotFoundException : Exception
    {
        protected NotFoundException(string message) : base(message) { }
    }
}
