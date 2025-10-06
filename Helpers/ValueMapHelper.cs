using SkiaSharp;

namespace DefBTGBrown.Helpers
{
    //Funções pra mapeamento do valor para a área do gráfico, nota, Y=0 é tipo leftmost
    //top. lembrando XPoint é largura e Y point é altura
    public static class ValueMapHelper
    {
        public static float YPoint(float value, float minPrice, float maxPrice, SKRect chartArea)
        {
            if (maxPrice == minPrice)
            {
                return chartArea.MidY;
            }

            return chartArea.Bottom - ((value - minPrice) / (maxPrice - minPrice)) * chartArea.Height;
        }

        public static float XPoint(float value, int maxTime, SKRect chartArea)
        {
            return chartArea.Left + (value / maxTime) * chartArea.Width;
        }
    }
}
