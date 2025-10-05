using SkiaSharp;

namespace DefBTGBrown.Models
{
    public partial class BrownianComponent
    {
        public double InitialPrice { get; set; }
        public double Volatilidade { get; set; }
        public double Media { get; set; }
        public int Time { get; set; }
        public double[] Values { get; set; }
        public SKColor Color { get; set; }
    }
}
