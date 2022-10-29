namespace YALLI.Extensions
{
    internal static class CommonExtensions
    {
        internal static bool IsDefault<T>(this T value)
            where T : struct
        {
            return value.Equals(default(T));
        }
    }
}
