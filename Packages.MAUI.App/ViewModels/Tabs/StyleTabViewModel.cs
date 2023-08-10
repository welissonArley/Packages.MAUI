using CommunityToolkit.Mvvm.ComponentModel;

namespace Packages.MAUI.App.ViewModels.Tabs;

public partial class StyleTabViewModel : ObservableObject
{
    [ObservableProperty]
    public string text;

    public StyleTabViewModel() => Text = "Text from ViewModel";
}
