using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DefBTGBrown.Models;
using SkiaSharp;
using SkiaSharp.Views.Maui.Controls;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace DefBTGBrown.ViewModels
{
    public partial class MainViewModel : ObservableValidator
    {
        [ObservableProperty]
        [Required(ErrorMessage = "O preço inicial é obrigatório.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O preço deve ser maior que zero.")]
        private double _initialPrice = 100;

        [ObservableProperty]
        [Required(ErrorMessage = "A volatilidade é obrigatória.")]
        [Range(0, 100, ErrorMessage = "A volatilidade deve ser entre 0 e 100.")]
        private double _volatilidade = 20;

        [ObservableProperty]
        [Required(ErrorMessage = "A média do retorno é obrigatória.")]
        private double _media = 5;

        [ObservableProperty]
        [Required(ErrorMessage = "O tempo é obrigatório.")]
        [Range(1, int.MaxValue, ErrorMessage = "O tempo deve ser de pelo menos 1 dia.")]
        private int _time = 252;


        [ObservableProperty]
        private ObservableCollection<BrownianComponent> _components = new();
        public SKCanvasView Skia;

        public double[] GenerateBrownianMotion(double sigma, double mean, double initialPrice, int numDays)
        {
            Random rand = new();
            double[] prices = new double[numDays];
            prices[0] = initialPrice;

            for (int i = 1; i < numDays; i++)
            {
                double u1 = 1.0 - rand.NextDouble();
                double u2 = 1.0 - rand.NextDouble();
                double z = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Cos(2.0 * Math.PI * u2);

                double retonoDiario = mean + sigma * z;

                prices[i] = prices[i - 1] * Math.Exp(retonoDiario);
            }

            return prices;
        }

        [RelayCommand(CanExecute = nameof(CanSimulate))]
        private void Simulate()
        {
            ValidateAllProperties();
            if (HasErrors)
            {
                return;
            }

            var graphic = new BrownianComponent
            {
                InitialPrice = this.InitialPrice,
                Media = this.Media,
                Volatilidade = this.Volatilidade,
                Time = this.Time,
                Values = GenerateBrownianMotion(
                    sigma: this.Volatilidade / 100,
                    initialPrice: this.InitialPrice,
                    numDays: this.Time,
                    mean: this.Media / 100),
                Color = GetRandomColor()
            };

            Components.Add(graphic);
        }

        private bool CanSimulate()
        {
            return !HasErrors;
        }

        protected override void OnPropertyChanged(System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
            if (e.PropertyName == nameof(HasErrors))
            {
                SimulateCommand.NotifyCanExecuteChanged();
            }
        }

        private SKColor GetRandomColor()
        {
            Random random = new();
            return random.Next(1, 6) switch
            {
                1 => SKColors.Yellow,
                2 => SKColors.Green,
                3 => SKColors.Red,
                4 => SKColors.Purple,
                5 => SKColors.Tomato,
                _ => SKColors.Teal
            };
        }

        public float YPoint(int height, float value, float maxValue)
        {
            var prop = height / maxValue;

            return height - (float)(prop * value);
        }

        public float XPoint(float value, float width, int totalItens)
        {
            var prop = (width / totalItens) * 0.90f;

            return value * prop;
        }


        public MainViewModel()
        {
            ValidateAllProperties();
        }
    }
}
