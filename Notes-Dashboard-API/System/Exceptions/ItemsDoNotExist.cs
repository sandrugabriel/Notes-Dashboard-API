namespace Notes_Dashboard_API.System.Exceptions
{
    public class ItemsDoNotExist:Exception
    {
        public ItemsDoNotExist(string? message):base(message) { }
    }
}
