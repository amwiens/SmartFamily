using SmartFamily.ViewModels;

namespace SmartFamily.Views;

public partial class NewDemoPage : ContentPage
{
	public NewDemoPage(NewDemoPageViewModel viewModel)
	{
		BindingContext = viewModel;
		InitializeComponent();
	}
}