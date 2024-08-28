namespace SimpleTrader.Domain.Exceptions
{
    public class InvalidPasswordException : Exception
    {
        public string Username { get; }
        public string Password { get; }

        public InvalidPasswordException(string? message, string username, string password) : base(message)
        {
            Username = username;
            Password = password;
        }

        public InvalidPasswordException(string? message, Exception? innerException, string username, string password) : base(message, innerException)
        {
            Username = username;
            Password = password;
        }

        public InvalidPasswordException(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }
}
