using System;

namespace Pojazdy
{
    /// <summary>
    /// Dane używane do konwertowania jednostek prędkości na ustandaryzowane kilometry na godzinę
    /// </summary>
    internal static class ConvertersData
    {
        public static readonly double MileNaGodzine = Math.Round(1.6093123, 2);
        public static readonly double MetryNaSekunde = Math.Round(3.6, 2);
    }
}