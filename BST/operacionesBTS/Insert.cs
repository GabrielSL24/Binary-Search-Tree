namespace BST_en_Disco
{
    public class InsertBSTOperation<T> where T : IComparable<T> //Se implementa generics
    {
        private string RutaArchivoBinario; //Ruta del archivo a crear
        private const long POSICION_RAIZ = 0; //Inicializamos la posición sobre la cual se inician las inserciones en el archivo.

        public InsertBSTOperation(string rutaArchivoBinario) //Constructor de la clase
        {
            RutaArchivoBinario = rutaArchivoBinario;
            Console.WriteLine($"Operación de inserción iniciada. Archivo: {RutaArchivoBinario}");
        }

        public void Insert(T valor)
        {
            Console.WriteLine($"Insertando valor: {valor}");
            if (IsEmpty()) //Se verifica si el archivo está vació o no
            {
                Console.WriteLine("Árbol vacío. Insertando en la raíz.");
                InsertarEnRaiz(valor); //Se inserta directamente en la posción 0 del archivo.
            }
            else
            {
                Console.WriteLine("Árbol no vacío. Navegando para insertar el nodo.");
                NavegarParaInsertarNodo(valor, POSICION_RAIZ); //Se procesa con las operaciones apra navegar entre el arbol en caso de que no esté vacío.
            }
        }

        private bool IsEmpty() //Para determinar el archivo está vacío.
        {
            bool isEmpty = new FileInfo(RutaArchivoBinario).Length == 0;
            Console.WriteLine($"Verificando si el árbol está vacío: {isEmpty}");
            return new FileInfo(RutaArchivoBinario).Length == 0;
        }

        private void InsertarEnRaiz(T valor) //Inserta en la posición inicial
        {
            var nodoRaiz = new TreeNode<T>(valor, POSICION_RAIZ);
            EscribirInfoNodo(nodoRaiz);
            Console.WriteLine($"Nodo raíz insertado con valor: {valor}");
        }

        private void NavegarParaInsertarNodo(T valor, long posicionActual) //Se encarga de navegar por los nodos haciendo comparaciones entre los mismos
        {//esto con la finalidad de saber insertar.

            Console.WriteLine($"Navegando. Posición actual: {posicionActual}");
            TreeNode<T> nodoActual = LeerNodoValor(posicionActual);
            Console.WriteLine($"Nodo actual: Valor={nodoActual.ValorAlmacenar}, Posición={nodoActual.PosicionActual}");

            if (valor.CompareTo(nodoActual.ValorAlmacenar) < 0)
            {
                Console.WriteLine("Valor a insertar es menor. Moviendo a la izquierda.");
                if (nodoActual.PosicionIzquierda == -1) //En el caso de que se encuentre el fin de una de las ramas del árbol
                {
                    Console.WriteLine("No hay hijo izquierdo. Insertando nuevo nodo.");
                    nodoActual.PosicionIzquierda = ObtenerPosicionFinalDelArchivo();
                    EscribirInfoNodo(nodoActual);
                    InsertarNuevoNodoEnPosicion(valor, nodoActual.PosicionIzquierda);
                }
                else //Sino, entonces se sigue navegando.
                {
                    Console.WriteLine($"Continuando a la izquierda. Nueva posición: {nodoActual.PosicionIzquierda}");
                    NavegarParaInsertarNodo(valor, nodoActual.PosicionIzquierda); //todo el rato hacía la izquierda
                }
            }
            else if (valor.CompareTo(nodoActual.ValorAlmacenar) > 0)//Mismo caso pero por derecho. 
            {
                Console.WriteLine("Valor a insertar es mayor. Moviendo a la derecha.");
                if (nodoActual.PosicionDerecha == -1)//Si se encuentra el fin de una de las ramas
                {
                    Console.WriteLine("No hay hijo derecho. Insertando nuevo nodo.");
                    nodoActual.PosicionDerecha = ObtenerPosicionFinalDelArchivo();
                    EscribirInfoNodo(nodoActual);
                    InsertarNuevoNodoEnPosicion(valor, nodoActual.PosicionDerecha);
                }
                else //Sino se continua navegando hacía la derecha todo el rato.
                {
                    Console.WriteLine($"Continuando a la derecha. Nueva posición: {nodoActual.PosicionDerecha}");
                    NavegarParaInsertarNodo(valor, nodoActual.PosicionDerecha);
                }
                
            }
            else            // Si el valor es igual, no hacemos nada (asumiendo que no permitimos duplicados)
            {
                Console.WriteLine("Valor duplicado. No se realiza la inserción.");
            }
        }

        private void InsertarNuevoNodoEnPosicion(T valor, long posicion)
        {
            Console.WriteLine($"Nuevo nodo insertado: Valor={valor}, Posición={posicion}");
            var nuevoNodo = new TreeNode<T>(valor, posicion);
            EscribirInfoNodo(nuevoNodo);
        }

        private TreeNode<T> LeerNodoValor(long posicion)
        {
            Console.WriteLine($"Leyendo nodo en posición: {posicion}");
            using (BinaryReader reader = new BinaryReader(File.Open(RutaArchivoBinario, FileMode.Open)))
            {
                reader.BaseStream.Seek(posicion, SeekOrigin.Begin);

                string lectorDeValores = reader.ReadString();
                T valor = (T)Convert.ChangeType(lectorDeValores, typeof(T));
                long posicionIzquierdaLector = reader.ReadInt64();
                long posicionDerechaLector = reader.ReadInt64();

                Console.WriteLine($"Nodo leído: Valor={valor}, Izq={posicionIzquierdaLector}, Der={posicionDerechaLector}");
                return new TreeNode<T>(valor, posicion)
                {
                    PosicionIzquierda = posicionIzquierdaLector,
                    PosicionDerecha = posicionDerechaLector
                };
            }
        }

        private void EscribirInfoNodo(TreeNode<T> nodo) //Escribe dentro del archivo los valores del nodo con sus propiedades
        {
            Console.WriteLine($"Escribiendo nodo: Valor={nodo.ValorAlmacenar}, Posición={nodo.PosicionActual}");
            using (BinaryWriter writer = new BinaryWriter(File.Open(RutaArchivoBinario, FileMode.Open)))
            {
                writer.Seek((int)nodo.PosicionActual, SeekOrigin.Begin);

                if (nodo.ValorAlmacenar != null)
                {
                    string? valorString = nodo.ValorAlmacenar.ToString();

                    if (!string.IsNullOrEmpty(valorString))
                    {
                        writer.Write(valorString);
                        writer.Write(nodo.PosicionIzquierda);
                        writer.Write(nodo.PosicionDerecha);
                        Console.WriteLine($"Nodo escrito: Valor={valorString}, Izq={nodo.PosicionIzquierda}, Der={nodo.PosicionDerecha}");
                    }
                    else
                    {
                        throw new InvalidOperationException("El valor esta vacio o es de tipo raro no aceptado");
                    }
                }
                else
                {
                    throw new InvalidOperationException("El valor del nodo no puede ser nulo.");
                }
            }
        }

        private long ObtenerPosicionFinalDelArchivo() //con cada registro la longitud aumenta, entonces lo que se hace es leer el largo,
        { //para obtener el tamaño.
            long posicionFinal = new FileInfo(RutaArchivoBinario).Length;
            Console.WriteLine($"Nueva posición para inserción: {posicionFinal}");
            return posicionFinal;
        }
    }
}