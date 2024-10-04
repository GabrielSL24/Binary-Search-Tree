using Entities;

namespace BST_en_Disco
{
    public class BinarySearchTree<T> where T : IComparable<T>
    {
        private string rutaArchivoBinario;

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

        //Método para leer un nodo desde un nodo en un a posición específica:
        public TreeNode<T> LeerNodoValor(long posicion)
        {
            using (BinaryReader reader = new BinaryReader(File.Open(rutaArchivoBinario, FileMode.Open)))
            {
                reader.BaseStream.Seek(posicion, SeekOrigin.Begin);

                string LectorDeValores = reader.ReadString();
                T valor = (T)Convert.ChangeType(LectorDeValores, typeof(T));
                long posicionIzquierdaLector = reader.ReadInt64();
                long posicionDerechaLector = reader.ReadInt64();

                return new TreeNode<T>(valor, posicion)
                {
                    PosicionIzquierda = posicionIzquierdaLector,
                    PosicionDerecha = posicionDerechaLector
                };
            }
        }

        public void EscribirInfoNodo(TreeNode<T> nodo)
        {
            using (BinaryWriter writer = new BinaryWriter(File.Open(rutaArchivoBinario, FileMode.OpenOrCreate)))
            {
                writer.Seek((int)nodo.PosicionActual, SeekOrigin.Begin);

                if (nodo.ValorAlmacenar != null)
                {
                    string? valorString = nodo.ValorAlmacenar.ToString();

                    if (!string.IsNullOrEmpty(valorString))
                    {
                        writer.Write(valorString);
                    }
                    else
                    {
                        throw new InvalidOperationException("El valor del nodo no tiene una representación válida.");
                    }
                }
                else
                {
                    throw new InvalidOperationException("El valor del nodo no puede ser nulo.");
                }

                writer.Write(nodo.PosicionIzquierda);
                writer.Write(nodo.PosicionDerecha);
            }
        }

        public void Insert(T valor)
        {
            long posicionFinal = ObtenerPosicionFinalDelArchivo();
            var insertOperation = new InsertBTSOperation<T>(valor, rutaArchivoBinario);
            insertOperation.InsertarNodo(posicionFinal);
        }

        private long ObtenerPosicionFinalDelArchivo()
        {
            return new FileInfo(rutaArchivoBinario).Length;
        }

        //public TreeNode<T> Search(T valor)
        //{
//
        //}
//
        //public void Delete(T valor)
        //{
        //    
        //}
    }
}
