using System.Globalization;

namespace Common.Utilities
{
    public static class DateFormatter
    {
        public static string ToSqlFormat(this DateTime dateTime)
        {
            return dateTime.ToString("yyyy-MM-dd HH:mm:ss");
        }

        public static DateTime ToCSharpDate(this string sqlDate)
        {
            return DateTime.ParseExact(sqlDate, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
        }
    }
}
