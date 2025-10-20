using System.Reflection;

namespace PinCodes.Authorization.Extensions;
internal static class ViewExtensions
{
    internal static T Clone<T>(this T originalShape) where T : View
    {
        var shapeType = originalShape.GetType();
        var newShape = (T)Activator.CreateInstance(shapeType)!;

        foreach (var property in shapeType.GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(property => property.CanWrite))
        {
            var value = property.GetValue(originalShape);
            property.SetValue(newShape, value);
        }

        return newShape;
    }
}