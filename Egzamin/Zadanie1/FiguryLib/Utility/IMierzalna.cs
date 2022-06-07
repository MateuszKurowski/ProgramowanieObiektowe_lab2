//file: IMierzalna.cs

namespace FiguryLib
{
    /// <summary>
    /// Figury mierzalne można skalować,
    /// czyli proporcjonalnie zmieniać ich rozmiar
    /// </summary>
    public interface IMierzalna
    {
        /// <summary>
        /// Zmienia rozmiar figury.
        /// </summary>
        /// <param name="wspSkalowania"> dodatnia wartość rzeczywista</param>
        void Skaluj(double wspSkalowania);
    }

    /// <summary>
    /// Figurami mierzalnymi w 1D są takie figury, które po ewentualnym rozcięciu
    /// można "umieścić" na osi (miara liniowa).
    /// Przykładami takich figur są m.in. odcinek, łamana, okrąg (ale nie koło), ... - umieszczone
    /// zarówno w 2D jak i w 3D, np. spirala przestrzenna
    /// </summary>
    public interface IMierzalna1D : IMierzalna
    {
        /// <summary>Zwraca rozmiar figury w 1D - zwyczajowa nazwa: długość</summary>
        double Dlugosc { get; }
    }

    /// <summary>
    /// Figury mierzalne w 2D - figury zamknięte, mające pole powierzchni.
    /// Przykładami takich figur są m.in. koło, prostokąt, trójkąt - umieszczone zarówno w 2D jak i w 3D lub np. sfera.
    /// Zwyczajowo, dla takich figur, nazwą miary powierzchniowej jest "pole".
    /// </summary>
    public interface IMierzalna2D : IMierzalna
    {
        /// <summary>Zwraca rozmiar powierzchniowy figury w 2D</summary>
        double Pole { get; }
    }

    /// <summary>
    /// Figury mierzalne w 3D - figury zamknięte, mające dodatkowo objętość.
    /// Przykładami takich figur są m.in. kula, prostopadłościan, ostrosłup, ...
    /// Przyjmujemy, że miara liniowa figury 3D zawsze wynosi 0.
    /// Figury 2D umieszczone w 3D maja objętość 0.
    /// </summary>
    public interface IMierzalna3D : IMierzalna
    {
        double Objetosc { get; }
    }
}