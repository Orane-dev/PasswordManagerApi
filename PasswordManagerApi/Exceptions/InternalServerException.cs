namespace PasswordManagerApi.Exceptions
{
    public class InternalServerException : Exception
    {
        public InternalServerException(string message) : base(message) { }  
    }
}
