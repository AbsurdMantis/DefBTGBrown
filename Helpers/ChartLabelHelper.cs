using SkiaSharp;

namespace DefBTGBrown.Helpers
{
    public static class ChartLabelHelper
    {
        public static void DrawAxisLabels(SKCanvas canvas, SKRect chartArea, float minPrice, float maxPrice, int maxTime)
        {
            using var paint = new SKPaint
            {
                Color = SKColors.Black,
                TextSize = 20,
                IsAntialias = true
            };

            int labelCount = 5;
            for (int i = 0; i < labelCount; i++)
            {
                float value = minPrice + (i * (maxPrice - minPrice) / (labelCount - 1));
                float y = ValueMapHelper.YPoint(value, minPrice, maxPrice, chartArea);
                string labelText = $"{value:C}";

                paint.TextAlign = SKTextAlign.Right;
                canvas.DrawText(labelText, chartArea.Left - 10, y + (paint.TextSize / 2), paint);
            }

            for (int i = 0; i < labelCount; i++)
            {
                float value = i * (float)maxTime / (labelCount - 1);
                float x = ValueMapHelper.XPoint(value, maxTime, chartArea);
                string labelText = $"{value:F0}";

                paint.TextAlign = SKTextAlign.Center;
                canvas.DrawText(labelText, x, chartArea.Bottom + 30, paint);
            }
        }

        public static void DrawAxes(SKCanvas canvas, SKRect chartArea)
        {
            using var paint = new SKPaint
            {
                Color = SKColors.Gray,
                StrokeWidth = 2
            };


            canvas.DrawLine(chartArea.Left, chartArea.Top, chartArea.Left, chartArea.Bottom, paint);
            canvas.DrawLine(chartArea.Left, chartArea.Bottom, chartArea.Right, chartArea.Bottom, paint);
        }
    }
}
