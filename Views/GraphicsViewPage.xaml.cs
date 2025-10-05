using DefBTGBrown.ViewModels;
using SkiaSharp;
using SkiaSharp.Views.Maui;

namespace DefBTGBrown.Views;

public partial class GraphicsViewPage : ContentPage
{
    private MainViewModel ViewModel;
    public GraphicsViewPage(MainViewModel viewModel)
    {
        ViewModel = viewModel;
        BindingContext = viewModel;
        InitializeComponent();
    }

    private void ChartView_PaintSurface(object sender, SKPaintSurfaceEventArgs e)
    {

        if (ViewModel?.Components == null || !ViewModel.Components.Any())
        {
            e.Surface.Canvas.Clear();
            return;
        }

        var surface = e.Surface;
        var canvas = surface.Canvas;
        canvas.Clear(SKColors.LightGray);
        var height = e.Info.Height;
        var width = e.Info.Width;

        foreach (var item in ViewModel.Components)
        {

            if (item.Values is null || item.Values.Length == 0)
            {
                continue;
            }

            var graphLinePaint = new SKPaint
            {
                Color = item.Color,
                StrokeWidth = 2,
                IsAntialias = true
            };

            var maxValue = (float)item.Values.Max();

            for (int i = 0; i < item.Values.Length - 1; i++)
            {
                var fromPoint = new SKPoint(
                    ViewModel.XPoint(i, width, item.Values.Length),
                    ViewModel.YPoint(height, (float)item.Values[i], maxValue)
                );

                var toPoint = new SKPoint(
                    ViewModel.XPoint(i + 1, width, item.Values.Length),
                    ViewModel.YPoint(height, (float)item.Values[i + 1], maxValue)
                );

                canvas.DrawLine(fromPoint, toPoint, graphLinePaint);
            }
        }
    }
}