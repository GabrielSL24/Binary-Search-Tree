namespace BST_en_Disco
{
    public class Delete<T> where T : IComparable<T>
    {
        private string RutaArchivoBinario;

        public Delete(string rutaArchivo)
        {
            RutaArchivoBinario = rutaArchivo;
        }

        public void Eliminar(T valor)
        {
            // Comenzamos la eliminación desde la raíz (posición 0)
            EliminarNodo(0, valor);
        }

        private long EliminarNodo(long posicionActual, T valor)
        {
            if (posicionActual == -1)
            {
                // El valor no se encontró en el árbol
                return -1;
            }

            // Leer el nodo actual desde el archivo
            TreeNode<T> nodoActual = LeerNodoDesdeArchivo(posicionActual);

            int comparacion = valor.CompareTo(nodoActual.ValorAlmacenar);

            if (comparacion < 0)
            {
                // Continuar la búsqueda en el subárbol izquierdo
                nodoActual.PosicionIzquierda = EliminarNodo(nodoActual.PosicionIzquierda, valor);
                EscribirNodoEnArchivo(nodoActual); // Actualizar el nodo en el archivo
            }
            else if (comparacion > 0)
            {
                // Continuar la búsqueda en el subárbol derecho
                nodoActual.PosicionDerecha = EliminarNodo(nodoActual.PosicionDerecha, valor);
                EscribirNodoEnArchivo(nodoActual); // Actualizar el nodo en el archivo
            }
            else
            {
                // Nodo encontrado, proceder a eliminar
                if (nodoActual.PosicionIzquierda == -1)
                {
                    // Caso 1: Nodo sin hijos o con un solo hijo derecho
                    return nodoActual.PosicionDerecha;
                }
                else if (nodoActual.PosicionDerecha == -1)
                {
                    // Caso 2: Nodo con un solo hijo izquierdo
                    return nodoActual.PosicionIzquierda;
                }
                else
                {
                    // Caso 3: Nodo con dos hijos
                    // Encontrar el sucesor (el valor más pequeño del subárbol derecho)
                    TreeNode<T> sucesor = ObtenerMinimo(nodoActual.PosicionDerecha);
                    nodoActual.ValorAlmacenar = sucesor.ValorAlmacenar;  // Reemplazar valor
                    nodoActual.PosicionDerecha = EliminarNodo(nodoActual.PosicionDerecha, sucesor.ValorAlmacenar);  // Eliminar el sucesor
                    EscribirNodoEnArchivo(nodoActual); // Actualizar el nodo en el archivo
                }
            }
            return posicionActual;
        }

        private TreeNode<T> ObtenerMinimo(long posicion)
        {
            // Navegar hacia la izquierda hasta encontrar el valor mínimo
            long posicionActual = posicion;
            TreeNode<T> nodo = LeerNodoDesdeArchivo(posicionActual);

            while (nodo.PosicionIzquierda != -1)
            {
                posicionActual = nodo.PosicionIzquierda;
                nodo = LeerNodoDesdeArchivo(posicionActual);
            }

            return nodo;
        }

        private TreeNode<T> LeerNodoDesdeArchivo(long posicion)
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

        private void EscribirNodoEnArchivo(TreeNode<T> nodo)
        {
            // Lógica para escribir el nodo actualizado en el archivo binario
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
