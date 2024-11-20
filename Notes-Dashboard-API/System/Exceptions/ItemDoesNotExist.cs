namespace Notes_Dashboard_API.System.Exceptions
{
    public class ItemDoesNotExist : Exception
    {
        public ItemDoesNotExist(string? message) : base(message) { }
    }
}
