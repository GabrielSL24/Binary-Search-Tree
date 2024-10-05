using Entities;

namespace BST_en_Disco
{
    public class BinarySearchTree<T> where T : IComparable<T>
    {
        private string rutaArchivoBinario;
        private const long POSICION_RAIZ = 0;

        public BinarySearchTree(string? rutaArchivo = null)
        {
            rutaArchivoBinario = rutaArchivo ?? ConfigPath.BinaryFilePath;
            InitializeSystem(rutaArchivoBinario);
        }

        private void InitializeSystem(string ruta)
        {
            ConfigPath.EnsureDirectoryExists();

            if (!File.Exists(ruta))
            {
                using (var fs = File.Create(ruta))
                {
                    Console.WriteLine($"Archivo creado en: {ruta}");
                }
            }
        }

        public void Insert(T valor)
        {
            var insertOperation = new InsertBSTOperation<T>(rutaArchivoBinario);
            insertOperation.Insert(valor);
        }

        // Métodos para Search y Delete se implementarían de manera similar
        public bool Search(T valor)
        {
            // Instancia de la operación de búsqueda
            var searchOperation = new BTSSearch<T>(rutaArchivoBinario);
            return searchOperation.Search(valor);
        }
    }
}