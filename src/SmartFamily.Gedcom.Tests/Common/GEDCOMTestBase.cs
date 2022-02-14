using System.IO;

namespace SmartFamily.Gedcom.Tests.Common
{
    public abstract class GEDCOMTestBase
    {
        protected virtual string EmbeddedFilePath
        {
            get => string.Empty;
        }

        protected virtual string FilePath
        {
            get => string.Empty;
        }

        protected string GetEmbeddedFileName(string fileName)
        {
            string fullName = $"{EmbeddedFilePath}.{fileName}";
            if (!fullName.ToLower().EndsWith(".ged"))
            {
                fullName += ".ged";
            }

            return fullName;
        }

        protected abstract Stream GetEmbeddedFileStream(string fileName);

        protected string GetEmbeddedFileString(string fileName)
        {
            string text = "";
            using (var reader = new StreamReader(GetEmbeddedFileStream(fileName)))
            {
                string line = "";
                while ((line = reader.ReadLine()) != null)
                {
                    text += $"{line}\n";
                }
            }
            return text;
        }

        private string GetFileName(string fileName)
        {
            string fullName = $"{FilePath}\\{fileName}";
            if (!fullName.ToLower().EndsWith(".ged"))
            {
                fullName += ".ged";
            }

            return fullName;
        }

        protected Stream GetFileStream(string fileName)
        {
            return new FileStream(GetFileName(fileName), FileMode.Open, FileAccess.Read);
        }

        protected string GetFileString(string fileName)
        {
            string text = "";
            using (StreamReader reader = new StreamReader(new FileStream(GetFileName(fileName), FileMode.Open, FileAccess.Read)))
            {
                string line = "";
                while ((line = reader.ReadLine()) != null)
                {
                    text += $"{line}\n";
                }
            }
            return text;
        }
    }
}