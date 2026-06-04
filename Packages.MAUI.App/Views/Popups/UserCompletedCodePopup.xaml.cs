using CommunityToolkit.Maui.Views;
using Packages.MAUI.App.ViewModels.Popups.UserCompletedCode;

namespace Packages.MAUI.App.Views.Popups;

public partial class UserCompletedCodePopup : Popup
{
    public UserCompletedCodePopup(UserCompletedCodeViewModel viewModel)
    {
        InitializeComponent();

        BindingContext = viewModel;
    }
}