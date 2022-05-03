namespace HealthAPI.Utils
{
    public sealed class CollectionByIdsBadRequestException : BadRequestException
    {
        public CollectionByIdsBadRequestException()
            : base("Collect count mistch comparing to ids.")
        { }
    }
}
