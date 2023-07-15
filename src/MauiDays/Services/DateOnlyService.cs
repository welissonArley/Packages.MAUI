namespace MauiDays.Services;

public class DateOnlyService
{
    private DateOnly _date;
    private Action _onHeaderChangeDate;

    public DateOnlyService() => SetDate(DateOnly.FromDateTime(DateTime.Today));

    public DateOnly GetDate() => _date;

    public void SetDate(DateOnly dateTime)
    {
        var oldDate = _date;
        _date = dateTime;
    }

    public void AddMonths(int month)
    {
        _date = _date.AddMonths(month);
        _onHeaderChangeDate?.Invoke();
    }

    public void AddYears(int years)
    {
        _date = _date.AddYears(years);
        _onHeaderChangeDate?.Invoke();
    }

    public void OnHeaderChangeDate(Action action) => _onHeaderChangeDate = action;
}
