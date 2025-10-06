using DefBTGBrown.Helpers;
using DefBTGBrown.ViewModels;
using SkiaSharp;
using SkiaSharp.Views.Maui;
using System.Collections.Specialized;

namespace DefBTGBrown.Views;

public partial class GraphicsViewPage : ContentPage
{
    private MainViewModel ViewModel;
    public GraphicsViewPage(MainViewModel viewModel)
    {
        ViewModel = viewModel;
        BindingContext = viewModel;
        InitializeComponent();
        this.Loaded += OnPageLoaded;
    }

    //Nota: forçando a UI a dar um refresh porque o skia não trigga sua renderização automaticamente no canvas
    private void OnPageLoaded(object? sender, EventArgs e)
    {
        if (ViewModel != null)
        {
            ViewModel.Components.CollectionChanged += OnComponentsCollectionChanged;
        }

        skiaCanvas.InvalidateSurface();
    }

    private void OnComponentsCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        skiaCanvas.InvalidateSurface();
    }

    private void ChartView_PaintSurface(object sender, SKPaintSurfaceEventArgs e)
    {
        var canvas = e.Surface.Canvas;
        canvas.Clear(SKColors.White);

        if (ViewModel?.Components is null || !ViewModel.Components.Any())
        {
            return;
        }

        float padding = 60;

        var chartArea = new SKRect(
            left: padding,
            top: padding / 2,
            right: e.Info.Width - padding / 2,
            bottom: e.Info.Height - padding);

        float minPrice = ViewModel.Components.SelectMany(c => c.Values).Min(v => (float)v);
        float maxPrice = ViewModel.Components.SelectMany(c => c.Values).Max(v => (float)v);
        int maxTime = ViewModel.Components.Max(c => c.Time);

        minPrice *= 0.95f;
        maxPrice *= 1.05f;

        ChartLabelHelper.DrawAxes(canvas, chartArea);
        DrawGraphLines(canvas, chartArea, minPrice, maxPrice, maxTime);
        ChartLabelHelper.DrawAxisLabels(canvas, chartArea, minPrice, maxPrice, maxTime);
    }

    private void DrawGraphLines(SKCanvas canvas, SKRect chartArea, float minPrice, float maxPrice, int maxTime)
    {
        foreach (var item in ViewModel.Components)
        {
            using var graphPaint = new SKPaint
            {
                Color = item.Color,
                StrokeWidth = 2,
                IsAntialias = true
            };

            for (int i = 0; i < item.Values.Length - 1; i++)
            {
                var fromPoint = new SKPoint(
                    ValueMapHelper.XPoint(i, maxTime, chartArea),
                    ValueMapHelper.YPoint((float)item.Values[i], minPrice, maxPrice, chartArea)
                );

                var toPoint = new SKPoint(
                    ValueMapHelper.XPoint(i + 1, maxTime, chartArea),
                    ValueMapHelper.YPoint((float)item.Values[i + 1], minPrice, maxPrice, chartArea)
                );

                canvas.DrawLine(fromPoint, toPoint, graphPaint);
            }
        }
    }


}