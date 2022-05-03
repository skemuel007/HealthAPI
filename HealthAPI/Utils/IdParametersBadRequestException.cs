namespace HealthAPI.Utils
{
    public sealed class IdParametersBadRequestException : BadRequestException
    {
        public IdParametersBadRequestException() : base("Paramter ids is null")
        {

        }
    }
}
