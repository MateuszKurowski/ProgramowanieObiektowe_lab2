using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace wellFormedTypes
{
    public static class Sortowanie
    {
        public static void SwapElements<T>(this IList<T> list, int firstIndex, int secondIndex)
        {
            Contract.Requires(list != null);
            Contract.Requires(firstIndex >= 0 && firstIndex < list.Count);
            Contract.Requires(secondIndex >= 0 && secondIndex < list.Count);
            if (firstIndex == secondIndex)
                return;

            T temp = list[firstIndex];
            list[firstIndex] = list[secondIndex];
            list[secondIndex] = temp;
        }

        public static void Sortuj<T>(this List<T> lista) where T : IComparable<T>
        {
            int n = lista.Count;
            do
            {
                for (int i = 0 ; i < n - 1 ; i++)
                {
                    if (lista[i].CompareTo(lista[i + 1]) > 0)
                    {
                        lista.SwapElements(i, i + 1);
                    }
                }
                n--;
            } while (n > 1);
        }

        public static void Sortuj<T>(this List<T> lista, IComparer<T> comparer)  
        {
            int n = lista.Count;
            do
            {
                for (int i = 0 ; i < n - 1 ; i++)
                {
                    if (comparer.Compare(lista[i], lista[i + 1]) > 0)
                        lista.SwapElements(i, i + 1);
                }
                n--;
            } while (n > 1);
        }

        public static void Sortuj<T>(this List<T> lista, Comparison<T> comparison)  
        {
            int n = lista.Count;
            do
            {
                for (int i = 0 ; i < n - 1 ; i++)
                {
                    if (comparison(lista[i], lista[i + 1]) > 0)
                        lista.SwapElements(i, i + 1);
                }
                n--;
            } while (n > 1);
        }
    }
}