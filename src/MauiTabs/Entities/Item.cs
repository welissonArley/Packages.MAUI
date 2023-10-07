using System.ComponentModel;

namespace MauiTabs.Entities;

public class Item : INotifyPropertyChanged
{
    public string Text { get; set; }
    public IView Content { get; set; }

    private bool isSelected;
    public bool IsSelected
    {
        set { isSelected = value; OnPropertyChanged(nameof(IsSelected)); }
        get { return isSelected; }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged(string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
