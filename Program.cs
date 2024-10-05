using BST_en_Disco;
using System;

public class Program
{
    public static void Main(string[] args)
    {
        var bst = new BinarySearchTree<int>();
    
        // Insertar valores en el árbol
        Console.WriteLine("Insertando valores: 5, 3, 7, 2, 4, 6, 8");
        bst.Insert(5);
        bst.Insert(3);
        bst.Insert(7);
        bst.Insert(2);
        bst.Insert(4);
        bst.Insert(6);
        bst.Insert(8);
        Console.WriteLine("Inserción completada. Revise los mensajes de consola para verificar el proceso.");

        // Realizar la búsqueda e imprimir el resultado
        bool result = bst.Search(8);
        Console.WriteLine($"Resultado de la búsqueda para 8: {(result ? "Encontrado" : "No encontrado")}");

        // Eliminar un valor
        Console.WriteLine("Eliminando el valor 3...");
        bst.Delete(3);
        Console.WriteLine("Eliminación completada.");

        // Volver a realizar la búsqueda para el valor eliminado
        result = bst.Search(3);
        Console.WriteLine($"Resultado de la búsqueda para 3 después de eliminar: {(result ? "Encontrado" : "No encontrado")}");
    }
}
