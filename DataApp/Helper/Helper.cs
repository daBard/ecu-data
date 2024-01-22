using System.Diagnostics;

namespace Helper
{
    internal class Helper
    {
        private readonly string _filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), @"log.txt");

        public void Logger(string methodName, string message)
        {
            try
            {
                using (var sw = new StreamWriter(_filePath, true))
                {
                    sw.WriteLine($"{methodName} - {message}");
                }
            }
            catch (Exception ex)
            {
                Debug.Write(ex.Message);
            }
        }
    }
}
