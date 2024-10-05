using BST_en_Disco;

public class Program
{
    public static void Main(string[] args)
    {
    var bst = new BinarySearchTree<int>();
    
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
    }
}