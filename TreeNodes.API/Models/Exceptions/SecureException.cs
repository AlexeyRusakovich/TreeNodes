namespace TreeNodes.API.Models.Exceptions
{
    public class SecureException : Exception
    {
        public SecureException(string message) : base(message) { }
    }
}
