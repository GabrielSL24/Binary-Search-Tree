namespace Entities
{
    public static class ConfigPath
    {
        //Así setteamos de forma global la ruta al archivo binario donde se almacena el árbol.

        public static string BinaryFileWhereBTSisCreated { get; } = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Binary-Search-Tree", "BinaryFile");
    }
}