using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Globalization;

namespace Packages.MAUI.App.ViewModels.Calendar;
public partial class SingleDaySelectorViewModel : ObservableObject
{
    [ObservableProperty]
    public DateOnly date;

    [ObservableProperty]
    public DateOnly minimumDate;

    [ObservableProperty]
    public DateOnly maximumDate;

    [ObservableProperty]
    public IList<int> daysWithEvents;

    [ObservableProperty]
    public CultureInfo culture;

    public SingleDaySelectorViewModel()
    {
        Culture = CultureInfo.CurrentCulture;

        var today = DateOnly.FromDateTime(DateTime.Today);

        Date = today;
        MinimumDate = new DateOnly(today.Year, today.Month - 1, 7);
        MaximumDate = new DateOnly(today.Year, today.Month + 1, 7);

        DaysWithEvents = new List<int> { 6, 11, 23, 24 };
    }

    [RelayCommand]
    public void SelectedDay(DateOnly date)
    {
        Date = date;
    }
}
