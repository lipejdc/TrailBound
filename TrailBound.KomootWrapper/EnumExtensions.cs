using System.ComponentModel;
using System.Reflection;

namespace TrailBound.KomootWrapper;

public static class EnumExtensions
{
    public static string GetDescription(this Enum value)
    {
        var field = value.GetType().GetField(value.ToString());
        if (field != null)
        {
            var attr = field.GetCustomAttribute<DescriptionAttribute>();
            if (attr != null)
                return attr.Description;
        }
        // fallback to enum name if no description
        return value.ToString();
    }
}
