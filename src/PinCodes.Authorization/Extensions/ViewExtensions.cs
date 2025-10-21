using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace PinCodes.Authorization.Extensions;

[UnconditionalSuppressMessage("Trimming", "IL2072", Justification = "Cloned view types are preserved via DynamicDependency.")]
[UnconditionalSuppressMessage("Trimming", "IL2075", Justification = "Members are preserved via DynamicDependency/linker.xml for cloned types.")]
internal static class ViewExtensions
{
    internal static T Clone<T>(this T original) where T : View
    {
        var shapeType = original.GetType();
        if(shapeType is null)
            return original;
        
        var newShape = (T)Activator.CreateInstance(shapeType)!;

        foreach (var p in shapeType.GetProperties(BindingFlags.Public | BindingFlags.Instance))
        {
            if (p.CanWrite == false)
                continue;

            p.SetValue(newShape, p.GetValue(original));
        }

        return newShape;
    }
}