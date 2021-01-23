namespace TntSearch.Core.Extensions
{
    public static class StringExtensions
    {
        public static string Unquote(this string self)
        {
            if (self.StartsWith("\"") && self.EndsWith("\""))
            {
                return self.Substring(1, self.Length - 2);
            }

            return self;
        }
    }
}
