using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DefBTGBrown.Helpers;
using DefBTGBrown.Models;
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
                Values = BrownianHelper.GenerateBrownianMotion(
                    sigma: this.Volatilidade / 100,
                    initialPrice: this.InitialPrice,
                    numDays: this.Time,
                    mean: this.Media / 100),
                Color = ChartColorHelper.GetRandomColor()
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

        public MainViewModel()
        {
            ValidateAllProperties();
        }
    }
}
