namespace Application.Extensions;

public static class StringExtensions
{
    public static bool HasValue(this string? value) => !string.IsNullOrEmpty(value);
    public static string GetValueOrDefault(this string? value) => (value.HasValue() ? value : string.Empty) ?? throw new InvalidOperationException("خطا در اعتبار سنجی");
}