using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace PinCodes.Authorization.Extensions;

[UnconditionalSuppressMessage("Trimming", "IL2070", Justification = "Members are preserved via DynamicDependency/linker.xml for cloned types.")]
[UnconditionalSuppressMessage("Trimming", "IL2072", Justification = "Cloned view types are preserved via DynamicDependency.")]
[UnconditionalSuppressMessage("Trimming", "IL2075", Justification = "Members are preserved via DynamicDependency/linker.xml for cloned types.")]
internal static class ViewExtensions
{
    private static readonly ConcurrentDictionary<Type, PropertyInfo[]> _writablePropertiesCache = new();

    internal static T Clone<T>(this T original) where T : View
    {
        var shapeType = original.GetType();

        var newShape = (T)Activator.CreateInstance(shapeType)!;

        var properties = _writablePropertiesCache.GetOrAdd(shapeType, static type =>
        {
            var allProperties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var writable = new List<PropertyInfo>(allProperties.Length);

            foreach (var property in allProperties)
            {
                if (property.CanWrite && property.GetIndexParameters().Length == 0)
                    writable.Add(property);
            }

            return writable.ToArray();
        });

        foreach (var property in properties)
            property.SetValue(newShape, property.GetValue(original));

        return newShape;
    }
}
