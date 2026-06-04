namespace PinCodes.Authorization.Views.Components.Keyboards;

public sealed class KeyDescription
{
    public string Key { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;
}

public sealed class KeyDescriptionCollection : List<KeyDescription>
{
}