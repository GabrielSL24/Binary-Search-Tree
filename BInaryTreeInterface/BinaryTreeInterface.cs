namespace BST_en_Disco
{
    public interface IBinarySearchTree<T> where T : IComparable<T>
    {
        // Método para insertar un valor en el árbol.
        void Insert(T value);

        // Método para eliminar un valor específico del árbol.
        void Delete(T value);

        // Método para buscar un valor específico en el árbol.
        bool Search(T value);
    }
}

/*
Explicación de los métodos:
Insert(T value): Inserta un valor en el árbol binario de búsqueda. Como es un árbol en disco, este método manejará la lógica para escribir 
el nuevo nodo en el archivo binario y actualizar las referencias (posiciones en el archivo).

Delete(T value): Elimina un valor específico del árbol. La eliminación es más compleja porque implica actualizar las referencias y manejar
adecuadamente los nodos hijos.

Search(T value): Busca un valor en el árbol. Devuelve true si el valor existe y false si no.
*/