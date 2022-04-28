using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pudelko
{
    internal sealed class Box
    {
        /// <summary>
        /// Wysokość pudełka
        /// </summary>
        public double Height { get; set; } = 10;
        /// <summary>
        /// Szerokość pudełka
        /// </summary>
        public double Width { get; set; } = 10;
        /// <summary>
        /// Długość pudełka
        /// </summary>
        public double Length { get; set; } = 10;

        /// <summary>
        /// Jednostka wymiaru pudełka
        /// </summary>
        public string Unit { get; set; } = "cm";
    }
}
