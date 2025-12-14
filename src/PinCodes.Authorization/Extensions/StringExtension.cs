using System.Diagnostics.CodeAnalysis;

namespace PinCodes.Authorization.Extensions;
internal static class StringExtension
{
    public static bool NotEmpty([NotNullWhen(true)] this string? value) => !string.IsNullOrWhiteSpace(value);
    public static bool IsEmpty([NotNullWhen(false)] this string? value) => string.IsNullOrWhiteSpace(value);
}
