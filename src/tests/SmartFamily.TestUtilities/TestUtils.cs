using System.Reflection;

namespace SmarFamily.TestUtilities
{
    public class TestUtils
    {
        public static string GetResourceFile(string fileName)
        {
            return (new StreamReader(Assembly.GetCallingAssembly().GetManifestResourceStream(fileName)).ReadToEnd());
        }
    }
}