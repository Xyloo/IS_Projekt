namespace IS_Projekt.Exceptions
{
    public class UsernameExistsException : Exception
    {
        public UsernameExistsException() : base("Username already exists") { }
    }
}
