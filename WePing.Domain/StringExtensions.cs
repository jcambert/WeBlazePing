using System.Globalization;
namespace WePing.Domain
{
    internal static  class StringExtensions
    {
        public static string ToTitle(this string value)
        {
            TextInfo ti= new CultureInfo("fr-FR", false).TextInfo;
            return ti.ToTitleCase(value);
        }
    }
}
