using static ver5.IDevice;

namespace ver5
{
    /// <summary>
    /// Obiekt imitujący faks
    /// </summary>
    public class Fax : IFax
    {
        /// <summary>
        /// Stan faksu
        /// </summary>
        State IDevice._State { get; set; }
    }
}