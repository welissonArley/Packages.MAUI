namespace Packages.MAUI.App;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        ShellPackagesApp.CurrentItem = DashboardSection;
    }
}
