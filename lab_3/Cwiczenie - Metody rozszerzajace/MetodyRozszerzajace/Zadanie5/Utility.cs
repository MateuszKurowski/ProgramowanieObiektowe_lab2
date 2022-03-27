using System.Collections.Generic;

namespace Extensions
{
    public static class Utility
    {
        public static double Mediana(this List<int> listaInt)
        {
            var lista = listaInt.ConvertAll(x => (double)x);
            lista.Sort();
            if (lista.Count % 2 == 0)
                return (lista[(lista.Count / 2) - 1] + lista[((lista.Count + 2) / 2) - 1]) / 2;
            else
                return lista[((lista.Count + 1) / 2) - 1];
        }
    }
}