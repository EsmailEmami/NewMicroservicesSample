namespace Application.Extensions;

public static class StringExtensions
{
    public static Guid ToGuid(this string value) => Guid.TryParse(value, out Guid result) ? result : Guid.Empty;

}