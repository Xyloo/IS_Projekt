namespace IS_Projekt.Exceptions
{
    public class EmailExistsException : Exception
    {
        public EmailExistsException() : base("Email already exists") { }
    }
}
