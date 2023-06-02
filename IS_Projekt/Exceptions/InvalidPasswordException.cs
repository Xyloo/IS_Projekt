namespace IS_Projekt.Exceptions
{
    public class InvalidPasswordException : Exception
    {
        public InvalidPasswordException() : base("Invalid password.") { }
    }
}
