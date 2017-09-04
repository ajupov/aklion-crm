using System;
using System.Globalization;

namespace Aklion.Infrastructure.Utils.DateTime
{
    public static class DateTimeExtension
    {
        private const string DateFormat = "dd.MM.yyyy";
        private const string IsoDateFormat = "yyyy-MM-dd";
        private const string TimeFormat = "HH:mm";
        private const string DateTimeFormat = "dd.MM.yyyy HH:mm";
        private const string DateTimeWithSecondsFormat = "dd.MM.yyyy HH:mm:ss";
        private const string DateWithoutYearFormat = "dd.MM";

        public static string ToDateString(this System.DateTime date)
        {
            return date.ToString(DateFormat);
        }

        public static string ToIsoDateString(this System.DateTime date)
        {
            return date.ToString(IsoDateFormat);
        }

        public static string ToTimeString(this System.DateTime time)
        {
            return time.ToString(TimeFormat);
        }

        public static string ToDateTimeString(this System.DateTime dateTime)
        {
            return dateTime.ToString(DateTimeFormat);
        }

        public static string ToDateTimeWithSecondsString(this System.DateTime dateTime)
        {
            return dateTime.ToString(DateTimeWithSecondsFormat);
        }

        public static string ToDateWithoutYearString(this System.DateTime date)
        {
            return date.ToString(DateWithoutYearFormat);
        }

        public static System.DateTime ToDate(this string dateString)
        {
            return System.DateTime.ParseExact(dateString, DateFormat, CultureInfo.InvariantCulture);
        }

        public static System.DateTime ToTime(this string timeString)
        {
            return System.DateTime.ParseExact(timeString, TimeFormat, CultureInfo.InvariantCulture);
        }

        public static System.DateTime ToTime(this int hour)
        {
            var today = System.DateTime.Today;
            return new System.DateTime(today.Year, today.Month, today.Day, hour, 0, 0);
        }

        public static System.DateTime ToDateTime(this string dateTimeString)
        {
            return System.DateTime.ParseExact(dateTimeString, DateTimeFormat, CultureInfo.InvariantCulture);
        }

        public static string ToWeekName(this DayOfWeek dayOfWeek)
        {
            switch (dayOfWeek)
            {
                case DayOfWeek.Monday:
                    return "Понедельник";
                case DayOfWeek.Tuesday:
                    return "Вторник";
                case DayOfWeek.Wednesday:
                    return "Среда";
                case DayOfWeek.Thursday:
                    return "Четверг";
                case DayOfWeek.Friday:
                    return "Пятница";
                case DayOfWeek.Saturday:
                    return "Суббота";
                case DayOfWeek.Sunday:
                    return "Воскресенье";
                default:
                    return string.Empty;
            }
        }

        public static string ToMonthName(this int month)
        {
            switch (month)
            {
                case 1:
                    return "Январь";
                case 2:
                    return "Февраль";
                case 3:
                    return "Март";
                case 4:
                    return "Апрель";
                case 5:
                    return "Май";
                case 6:
                    return "Июнь";
                case 7:
                    return "Июль";
                case 8:
                    return "Август";
                case 9:
                    return "Сентябрь";
                case 10:
                    return "Октябрь";
                case 11:
                    return "Ноябрь";
                case 12:
                    return "Декабрь";
                default:
                    return string.Empty;
            }
        }

        public static System.DateTime FirstDayOfYear(this System.DateTime date)
        {
            return new System.DateTime(date.Year, 1, 1);
        }
    }
}