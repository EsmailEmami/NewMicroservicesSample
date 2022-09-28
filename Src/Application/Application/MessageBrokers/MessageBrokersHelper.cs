using Domain.Core.Events;

namespace Application.MessageBrokers;

public static class MessageBrokersHelper
{
    public static string GetTypeName(Type type)
    {
        var name = type.FullName?.ToLower().Replace("+", ".");

        if (type == typeof(IEvent))
        {
            name += "_event";
        }

        return name;
    }

    public static string GetTypeName<T>()
    {
        return GetTypeName(typeof(T));
    }
}