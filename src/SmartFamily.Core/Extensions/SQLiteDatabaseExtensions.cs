namespace SmartFamily.Core.Extensions
{
    public static class SQLiteDatabaseExtensions
    {
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