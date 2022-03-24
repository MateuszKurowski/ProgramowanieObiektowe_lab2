using static ver5.IDevice;

namespace ver5
{
    /// <summary>
    /// Obiekt imitujący skaner
    /// </summary>
    public class Scanner : IScanner
    {
        /// <summary>
        /// Stan skanera
        /// </summary>
        State IDevice._State { get; set; }
    }
}