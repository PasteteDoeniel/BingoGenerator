using System.IO;
using System.Numerics;
using System.Reflection;

namespace BingoGenerator
{
    static class Constants
    {
        public static readonly int DefaultRowCount = 5;
        public static readonly float DefaultCellSize = 155;
        public static readonly int DefaultFontSize = 70;
        public static readonly Vector2 DefaultMargin = new Vector2(1, 40);
        public static readonly bool DefaultIsBold = true;
        public static readonly string DefaultCustomFields = "3/3:Frei";
        public static readonly string DefaultNumbers = "1-15, 16-30, 31-45, 46-60, 61-75";

        public const string SaveFileName = "SaveFile.txt";

        public static string GetApplicationPath()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();

            string assemblyFullName = assembly.FullName;
            string[] parts = assemblyFullName.Split(',');
            string applicationName = parts[0];

            string localAppDataFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string appFolderPath = Path.Combine(localAppDataFolderPath, applicationName);

            if (!Directory.Exists(appFolderPath))
            {
                Directory.CreateDirectory(appFolderPath);
            }

            return appFolderPath;
        }
    }
}
