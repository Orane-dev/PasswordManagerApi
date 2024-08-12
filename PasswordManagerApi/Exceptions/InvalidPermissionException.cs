namespace PasswordManagerApi.Exceptions
{
    public class InvalidPermissionException : Exception
    {
        public InvalidPermissionException(string message) : base(message) { }
    }
}
