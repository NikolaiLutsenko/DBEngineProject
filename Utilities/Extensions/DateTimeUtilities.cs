using System;

namespace Utilities.Extensions
{
    public static class DateTimeUtilities
    {

        public static string GetStringDate(this DateTime? sourse, string format)
        {
            return sourse.HasValue ? sourse.Value.ToString(format) : string.Empty;
        }

        public static string GetStringDate(this DateTime? sourse)
        {
            return sourse.GetStringDate("dd.MM.yyyy");
        }

        public static DateTime? ToNullableDate(this DateTime sourse)
        {
            return sourse == DateTime.MinValue ? null : (DateTime?)sourse;
        }

    }
}
