namespace SmartFamily.Core.Extensions
{
    /// <summary>
    /// SQLite database extensions
    /// </summary>
    public static class SQLiteDatabaseExtensions
    {
        /// <summary>
        /// Checks to see if the file is a SQLite database.
        /// </summary>
        /// <param name="databasePath">Path to the database.</param>
        /// <returns><c>true</c> if the file is a database, otherwise <c>false</c>.</returns>
        public static bool IsSQLiteDatabase(this string databasePath)
        {
            bool result = false;

            if (File.Exists(databasePath))
            {
                using (FileStream stream = new FileStream(databasePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    byte[] header = new byte[16];

                    for (int i = 0; i < 16; i++)
                    {
                        header[i] = (byte)stream.ReadByte();
                    }

                    result = System.Text.Encoding.UTF8.GetString(header).Contains("SQLite format 3");

                    stream.Close();
                }
            }

            return result;
        }
    }
}