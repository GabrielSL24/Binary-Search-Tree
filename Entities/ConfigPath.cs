
namespace Entities
{
    public static class ConfigPath
    {
        public static string BinaryTreeFolderName { get; } = "Binary-Search-Tree";
        public static string BinaryFileFolderName { get; } = "BinaryFile";
        public static string BinaryFileName { get; } = "BinaryFile.bin";

        public static string BinaryTreeFolderPath => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Trabajar tarea", BinaryTreeFolderName);
        public static string BinaryFileFolderPath => Path.Combine(BinaryTreeFolderPath, BinaryFileFolderName);
        public static string BinaryFilePath => Path.Combine(BinaryFileFolderPath, BinaryFileName);

        public static void EnsureDirectoryExists()
        {
            if (!Directory.Exists(BinaryFileFolderPath))
            {
                Directory.CreateDirectory(BinaryFileFolderPath);
                Console.WriteLine($"Directorio creado en: {BinaryFileFolderPath}");
            }
        }
    }
}
