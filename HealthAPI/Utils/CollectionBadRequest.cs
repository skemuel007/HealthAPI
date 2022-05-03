namespace HealthAPI.Utils
{
    public sealed class CollectionBadRequest<T> : BadRequestException
    {
        public CollectionBadRequest(T collectionObject) : base($"{nameof(collectionObject)} collect sent from sent form a client is null")
        {

        }
    }
}
