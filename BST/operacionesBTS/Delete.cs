namespace BST_en_Disco
{
    public class Delete<T> where T : IComparable<T>
    {
        private string RutaArchivoBinario; // Ruta del archivo binario en el que se almacenan los nodos

        // Constructor que inicializa la ruta del archivo binario
        public Delete(string rutaArchivo) 
        {
            RutaArchivoBinario = rutaArchivo;
        }

        // Método principal que comienza el proceso de eliminación del valor
        public void Eliminar(T valor)
        {
            // Comienza la eliminación desde la raíz (posición 0 en el archivo)
            EliminarNodo(0, valor);
        }

        // Método recursivo para eliminar el nodo que contiene el valor dado
        private long EliminarNodo(long posicionActual, T valor)
        {
            // Si la posición actual es -1, no se encontró el nodo, retornar -1
            if (posicionActual == -1)
            {
                return -1;
            }

            // Leer el nodo actual desde el archivo en la posición indicada
            TreeNode<T> nodoActual = LeerNodoDesdeArchivo(posicionActual);

            // Comparar el valor del nodo actual con el valor a eliminar
            int comparacion = valor.CompareTo(nodoActual.ValorAlmacenar);

            // Si el valor a eliminar es menor, continuar la búsqueda en el subárbol izquierdo
            if (comparacion < 0)
            {
                nodoActual.PosicionIzquierda = EliminarNodo(nodoActual.PosicionIzquierda, valor);
                EscribirNodoEnArchivo(nodoActual); // Actualizar el nodo en el archivo
            }
            // Si el valor a eliminar es mayor, continuar la búsqueda en el subárbol derecho
            else if (comparacion > 0)
            {
                nodoActual.PosicionDerecha = EliminarNodo(nodoActual.PosicionDerecha, valor);
                EscribirNodoEnArchivo(nodoActual); // Actualizar el nodo en el archivo
            }

            else // Si el valor coincide con el nodo actual, proceder a eliminar
            {
                // Caso 1: Nodo sin hijos o con un solo hijo derecho
                if (nodoActual.PosicionIzquierda == -1)
                {
                    return nodoActual.PosicionDerecha;
                }
                // Caso 2: Nodo con un solo hijo izquierdo
                else if (nodoActual.PosicionDerecha == -1)
                {
                    return nodoActual.PosicionIzquierda;
                }
                // Caso 3: Nodo con dos hijos
                else
                {
                    // Encontrar el sucesor inorden (el valor más pequeño en el subárbol derecho)
                    TreeNode<T> sucesor = ObtenerMinimo(nodoActual.PosicionDerecha);
                    nodoActual.ValorAlmacenar = sucesor.ValorAlmacenar;  // Reemplazar valor
                    // Eliminar el sucesor (que será un nodo hoja o con un solo hijo)
                    nodoActual.PosicionDerecha = EliminarNodo(nodoActual.PosicionDerecha, sucesor.ValorAlmacenar);
                    EscribirNodoEnArchivo(nodoActual); // Actualizar el nodo en el archivo
                }
            }
            return posicionActual; // Retornar la posición actual después de la eliminación
        }

        // Método para obtener el nodo con el valor mínimo (sucesor inorden)
        private TreeNode<T> ObtenerMinimo(long posicion)
        {
            long posicionActual = posicion;
            TreeNode<T> nodo = LeerNodoDesdeArchivo(posicionActual);

            // Continuar hacia la izquierda hasta encontrar el nodo más pequeño
            while (nodo.PosicionIzquierda != -1)
            {
                posicionActual = nodo.PosicionIzquierda;
                nodo = LeerNodoDesdeArchivo(posicionActual);
            }

            return nodo; // Retornar el nodo con el valor mínimo
        }

        // Método para leer un nodo del archivo binario en una posición específica
        private TreeNode<T> LeerNodoDesdeArchivo(long posicion)
        {
            using (BinaryReader reader = new BinaryReader(File.Open(RutaArchivoBinario, FileMode.Open)))
            {
                reader.BaseStream.Seek(posicion, SeekOrigin.Begin);

                // Leer el valor del nodo y las posiciones de sus hijos
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

        // Método para escribir la información del nodo actualizado en el archivo binario
        private void EscribirNodoEnArchivo(TreeNode<T> nodo)
        {
            using (BinaryWriter writer = new BinaryWriter(File.Open(RutaArchivoBinario, FileMode.Open)))
            {
                writer.BaseStream.Seek(nodo.PosicionActual, SeekOrigin.Begin);
                writer.Write(nodo.ValorAlmacenar.ToString());
                writer.Write(nodo.PosicionIzquierda);
                writer.Write(nodo.PosicionDerecha);
            }
        }
    }
}
