using System.ComponentModel;

namespace Notes_Dashboard_API.System.Exceptions
{
    public class InvalidDate : Exception
    {
        public InvalidDate(string? message):base(message) { }

    }
}
