namespace MauiDays.Services;
public class DateOnlyService
{
    private DateOnly _date;
    private Action _onChangedMonth;
    private Action _onChangedYear;
    private Action _onChoseDayInNextOrPreviousMonth;

    public DateOnlyService() => SetDate(DateOnly.FromDateTime(DateTime.Today));

    public DateOnly GetDate() => _date;

    public void SetDate(DateOnly dateTime)
    {
        var oldDate = _date;
        _date = dateTime;

        if (oldDate.Month != dateTime.Month && _onChoseDayInNextOrPreviousMonth is not null)
            _onChoseDayInNextOrPreviousMonth();

    }

    public void AddMonths(int month)
    {
        _date = _date.AddMonths(month);
        if (_onChangedMonth is not null)
            _onChangedMonth();
    }

    public void AddYears(int years)
    {
        _date = _date.AddYears(years);
        if (_onChangedYear is not null)
            _onChangedYear();
    }

    public void OnChangedMonth(Action onChange) => _onChangedMonth = onChange;
    public void OnChangedYear(Action onChange) => _onChangedYear = onChange;
    public void OnChoseDayInNextOrPreviousMonth(Action onChange) => _onChoseDayInNextOrPreviousMonth = onChange;
}
