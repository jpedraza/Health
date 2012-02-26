namespace Support
{
    internal static class ExtensionMethods
    {
        internal static string F(this string str, params string[] args)
        {
            return string.Format(str, args);
        }
    }
}