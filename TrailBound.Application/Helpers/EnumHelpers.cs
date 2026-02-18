using TrailBound.Domain.Enums;

namespace TrailBound.Application.Helpers;

/// <summary>
/// Provides methods to validate and parse Activity-related enums from strings.
/// </summary>

public static class EnumHelpers
{
    public static ActivityType ParseActivityType(string type)
    {
        //If the string cannot be converted to a valid ActivityType, throw an exception
        if (!Enum.TryParse<ActivityType>(type, true, out var result))
        {
            throw new ArgumentException($"Invalid Activity Type: {type}!");
        }

        return result;
    }

    public static ActivityStatus ParseActivityStatus(string status)
    {
        //If the string cannot be converted to a valid ActivityStatus, throw an exception
        if (!Enum.TryParse<ActivityStatus>(status, true, out var result))
        {
            throw new ArgumentException($"Invalid Activity Status: {status}!");
        }

        return result;
    }
}
