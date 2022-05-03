using System;

namespace HealthAPI.Utils
{
    public abstract class BadRequestException : Exception
    {
        protected BadRequestException(string message) : base(message)
        {

        }
    }
}
