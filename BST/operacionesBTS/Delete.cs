using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BST_en_Disco.BST.operacionesBTS
{
    public class DeleteBSTOperation<T> where T : IComparable<T>
    {
        //private string RutaArchivoBinario; //Ruta del archivo a crear
        //
        //public DeleteBSTOperation (string rutaArchivoBinario)
        //{
        //    RutaArchivoBinario = rutaArchivoBinario;
        //    Console.WriteLine($"Operación de eliminación iniciada. Archivo: {RutaArchivoBinario}");
        //}
//
        //public void Delete(T valor)
        //{
        //    Console.WriteLine($"Valor a eliminar: {valor}");
        //    if (valor == null) //Aquí no va valor y null sino el de search que si es verdadero
        //    {
        //        Console.WriteLine("Valor si existente");
        //        var posiciónAuxiliar = 0;
        //        IdentificarDelete(valor, posiciónAuxiliar);
        //    }
        //    else
        //    {
        //        Console.WriteLine("El valor ingresado no existe");
        //    }
        //}
//
        //private void IdentificarDelete(T valor, long posiciónActual)
        //{
        //    Console.WriteLine($"Verificando si {valor} tiene hijos");
        //    TreeNode<T> nodoActual = LeerNodoValor(posiciónActual);
        //    Console.WriteLine($"Nodo actual: Valor={nodoActual.ValorAlmacenar}, Posición={nodoActual.PosicionActual}");
//
        //    if (nodoActual.PosicionIzquierda != -1 & nodoActual.PosicionDerecha != -1)
        //    {
        //        //Lógica para dos hijos
        //    }
        //    else if (nodoActual.PosicionIzquierda == -1)
        //    {
        //        //Se hace para hijo izquierdo
        //    }
        //    else if (nodoActual.PosicionDerecha == -1)
        //    {
        //        //Hijo derecho
        //    }
        //    else
        //    {
//
        //    }
        //}
    }
}
