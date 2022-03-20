using static ver5.IDevice;

namespace ver5
{
    /// <summary>
    /// Obiekt imitujący drukarke
    /// </summary>
    public class Printer : IPrinter
    {
        /// <summary>
        /// Stan drukarki
        /// </summary>
        State IDevice._State { get; set; }
    }
}