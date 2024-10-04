namespace BST_en_Disco
{
    //La estructura principal se base sobre un archivo binario, se usan generics
    public class BinarySearchTree<T> where T : IComparable<T> 
    {
        
        private string rutaArchivoBinario;

        public BinarySearchTree()
        {
            rutaArchivoBinario = Entities.ConfigPath.BinaryFileWhereBTSisCreated;

            //Creamos el archivo si no existe.
            string? directorio = Path.GetDirectoryName(rutaArchivoBinario);

            if (directorio != null && !Directory.Exists(directorio))
            {
                Directory.CreateDirectory(directorio);
                Console.WriteLine($"Directorio creado en: {directorio}");
            }

            if (!File.Exists(rutaArchivoBinario))
            {
                using (var fs = File.Create(rutaArchivoBinario))
                {
                    Console.WriteLine($"Archivo creado en: {rutaArchivoBinario}");
                }
            }
        }

        public static class ConfigPath
        {
            public static string FilePath { get; set; } = "/ruta/por/defecto/archivo.dat";
        }
        
        //Método para leer un nodo desde un nodo en un a posición específica:
        public TreeNode<T> LeerNodoValor(long posicion)
        {
            using (BinaryReader reader = new BinaryReader(File.Open(rutaArchivoBinario, FileMode.Open)))
            {

                //Como se tiene que leer el valor específico de un nodo, movemos el lector a la osición justa del mismo
                reader.BaseStream.Seek(posicion, SeekOrigin.Begin);

                //Se procede a leer desde esa posición:
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

        //Método utilizado para escribir la información de un nodo en una posición dentro del archivo.
        public void EscribirInfoNodo(TreeNode<T> nodo)
        {
            using (BinaryWriter writer = new BinaryWriter(File.Open(rutaArchivoBinario, FileMode.OpenOrCreate)))
            {
                // Se mueve el lector/escritor a la posición del nodo en el archivo.
                writer.Seek((int)nodo.PosicionActual, SeekOrigin.Begin);

                if (nodo.ValorAlmacenar != null)
                {
                    string? valorString = nodo.ValorAlmacenar.ToString();

                    if (!string.IsNullOrEmpty(valorString))
                    {
                        writer.Write(valorString);  // Escribir el valor si no es nulo o vacío
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

                // Escribir las posiciones izquierda y derecha del nodo.
                writer.Write(nodo.PosicionIzquierda);
                writer.Write(nodo.PosicionDerecha);
            }
        }
    }
}
