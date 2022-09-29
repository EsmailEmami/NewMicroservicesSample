namespace Application.Extensions;

public static class TypeExtensions
{
    public static Type[] PrependToParamArray(this Type me, params Type[] args) => new[] { me }.Concat(args).ToArray();
}