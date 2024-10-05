namespace BST_en_Disco
{
    //Se implementa Generics para poder insertar cualquier tipo de datos
    public class TreeNode<T> where T : IComparable<T>
    {
        public T ValorAlmacenar {get;set;} //Valor del nodo actual
        public long PosicionIzquierda {get;set;} //Posición del hijo izquierdo
        public long PosicionDerecha {get;set;} //Posición del hijo derecho
        public long PosicionActual {get;set;} //Valor de la posición del nodo en el archivo

        public TreeNode(T valor, long posicionActual)
        {
            ValorAlmacenar = valor;
            PosicionActual = posicionActual;
            PosicionDerecha = -1;//Se inician al equivalente a null.
            PosicionIzquierda = -1;//Se inician al equivalente a null.
        }

    }
}
