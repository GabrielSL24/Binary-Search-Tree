namespace BST_en_Disco
{
    public class InsertBTSOperation<T> where T : IComparable<T> 
    {
        private T Node;
        private string RutaArchivoBinario;

        public InsertBTSOperation(T node, string rutaArchivoBinario)
        {
            Node = node;
            RutaArchivoBinario = rutaArchivoBinario;
        }

        //Método para realizar la inserción
        public void InsertarNodo(long posicionActual)
        {
            try
            {
                // Crear el nuevo nodo en la posición dada
                TreeNode<T> nuevoNodo = new TreeNode<T>(Node, posicionActual);

                // Abrir el archivo binario para escribir el nodo
                using (BinaryWriter writer = new BinaryWriter(File.Open(RutaArchivoBinario, FileMode.OpenOrCreate)))
                {
                    // Mover el escritor a la posición del nodo
                    writer.Seek((int)nuevoNodo.PosicionActual, SeekOrigin.Begin);

                    // Escribir los datos del nodo (Valor, PosiciónIzquierda, PosiciónDerecha)
                    string valorString = nuevoNodo.ValorAlmacenar?.ToString() ?? string.Empty;
                    writer.Write(valorString); // Escribir el valor del nodo
                    writer.Write(nuevoNodo.PosicionIzquierda);
                    writer.Write(nuevoNodo.PosicionDerecha);

                    Console.WriteLine("Se ha ingresado el valor al árbol de forma correcta.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al intentar insertar el nodo: {ex.Message}");
            }
        }
    }
}