using System.ComponentModel;

namespace Application.Extensions;

public static class ConvertExtensions
{
    public static TResult Parse<TResult>(this object value, TResult defaultValue = default!)
    {
        try
        {
            return (TResult)TypeDescriptor.GetConverter(typeof(TResult)).ConvertFrom(value)! ?? defaultValue;
        }
        catch
        {
            return defaultValue;
        }
    }
}