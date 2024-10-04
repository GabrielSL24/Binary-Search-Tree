namespace BST_en_Disco
{
    public interface IBinarySearchTree<T> where T : IComparable<T>
    {
        // Método para insertar un valor en el árbol.
        void Insert(T value);

        // Método para eliminar un valor específico del árbol.
        bool Delete(T value);

        // Método para buscar un valor específico en el árbol.
        bool Search(T value);

        // Método para obtener el valor mínimo almacenado en el árbol.
        T GetMin();

        // Método para obtener el valor máximo almacenado en el árbol.
        T GetMax();

        // Método para obtener la altura del árbol.
        int GetHeight();

        // Método para mostrar el recorrido en in-order.
        void InOrderTraversal(Action<T> action);

        // Método para mostrar el recorrido en pre-order.
        void PreOrderTraversal(Action<T> action);

        // Método para mostrar el recorrido en post-order.
        void PostOrderTraversal(Action<T> action);
    }
}

/*
Explicación de los métodos:
Insert(T value): Inserta un valor en el árbol binario de búsqueda. Como es un árbol en disco, este método manejará la lógica para escribir 
el nuevo nodo en el archivo binario y actualizar las referencias (posiciones en el archivo).

Delete(T value): Elimina un valor específico del árbol. La eliminación es más compleja porque implica actualizar las referencias y manejar
adecuadamente los nodos hijos.

Search(T value): Busca un valor en el árbol. Devuelve true si el valor existe y false si no.

GetMin(): Devuelve el valor mínimo del árbol, que es el nodo más a la izquierda.

GetMax(): Devuelve el valor máximo del árbol, que es el nodo más a la derecha.

GetHeight(): Calcula y devuelve la altura del árbol binario de búsqueda.

InOrderTraversal(Action<T> action): Realiza un recorrido in-order por el árbol (izquierda, nodo, derecha) y ejecuta una acción en cada nodo.

PreOrderTraversal(Action<T> action): Realiza un recorrido pre-order por el árbol (nodo, izquierda, derecha).

PostOrderTraversal(Action<T> action): Realiza un recorrido post-order por el árbol (izquierda, derecha, nodo).
*/