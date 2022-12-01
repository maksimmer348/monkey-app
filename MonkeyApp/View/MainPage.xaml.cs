using MonkeyApp.VM;

namespace MonkeyApp;

public partial class MainPage : ContentPage
{
	int count = 0;
    public MainPage(MonkeysVM viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}

