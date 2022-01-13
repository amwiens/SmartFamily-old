namespace SmartFamily.Core.Exceptions
{
    public class DatabaseFormatException : Exception
    {
        public string FileName { get; }

        public DatabaseFormatException(string fileName)
        {
            FileName = fileName;
        }

        public DatabaseFormatException(string? message, string fileName) : base(message)
        {
            FileName = fileName;
        }

        public DatabaseFormatException(string? message, Exception? innerException, string fileName)
        {
            FileName = fileName;
        }
    }
}