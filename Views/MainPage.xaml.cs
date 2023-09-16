namespace Fit4Time.Views;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
        
        var vm = MyServiceProvider.GetService<MainPageViewModel>();
        this.BindingContext = vm;

        _ = vm.GetWorkoutAsync();
    }
}
