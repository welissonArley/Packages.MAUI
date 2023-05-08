using Packages.MAUI.App.ViewModels.Calendar;

namespace Packages.MAUI.App.Views.Calendar;

public partial class SingleDaySelectorPage : MauiDays.Views.Pages.SingleDaySelectorPage
{
	public SingleDaySelectorPage(SingleDaySelectorViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel;
	}
}