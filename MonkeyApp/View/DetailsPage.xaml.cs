using Microsoft.Maui.Controls;
using MonkeyApp.Model;
using MonkeyApp.VM;

namespace MonkeyApp.View;

public partial class DetailsPage : ContentPage
{
    public Monkey Monkey { get; set; }

    public DetailsPage(MonkeyDetailsVM viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
    }
}