using DefBTGBrown.ViewModels;

namespace DefBTGBrown.Views;

public partial class GraphicsViewPage : ContentPage
{
    private MainViewModel viewModel;
    public GraphicsViewPage(MainViewModel ViewModel)
    {
        viewModel = ViewModel;
        BindingContext = viewModel;
        InitializeComponent();
    }
}