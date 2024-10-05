namespace BST_en_Disco
{
    public class BTSSearch<T> where T : IComparable<T>
    {
        private string RutaArchivoBinario;

        public BTSSearch(string rutaArchivo)
        {
            RutaArchivoBinario = rutaArchivo;
        }

        public bool Search(T valor)
        {
            // Comenzamos la búsqueda desde la posición raíz (0)
            return SearchInFile(0, valor);
        }

        private bool SearchInFile(long posicionActual, T valor)
        {
            if (posicionActual == -1)
            {
                // No se encontró el valor
                return false;
            }

            // Leer el nodo actual del archivo
            TreeNode<T> nodoActual = ReadNodeFromFile(posicionActual);

            int comparacion = valor.CompareTo(nodoActual.ValorAlmacenar);

            if (comparacion == 0)
            {
                // Valor encontrado
                return true;
            }
            else if (comparacion < 0)
            {
                // Buscar en la rama izquierda
                return SearchInFile(nodoActual.PosicionIzquierda, valor);
            }
            else
            {
                // Buscar en la rama derecha
                return SearchInFile(nodoActual.PosicionDerecha, valor);
            }
        }

        private TreeNode<T> ReadNodeFromFile(long posicion)
        {
            // Lógica para leer el nodo desde el archivo binario en la posición 'posicion'
            using (BinaryReader reader = new BinaryReader(File.Open(RutaArchivoBinario, FileMode.Open)))
            {
                reader.BaseStream.Seek(posicion, SeekOrigin.Begin);

                T valor = (T)Convert.ChangeType(reader.ReadString(), typeof(T));
                long posicionIzquierda = reader.ReadInt64();
                long posicionDerecha = reader.ReadInt64();

                return new TreeNode<T>(valor, posicion)
                {
                    PosicionIzquierda = posicionIzquierda,
                    PosicionDerecha = posicionDerecha
                };
            }
        }
    }
}
