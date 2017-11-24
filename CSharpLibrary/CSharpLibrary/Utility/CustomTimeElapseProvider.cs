using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpLibrary.Utility
{
    public enum ElapsedTimeFormat
    {
        Days,
        Hours,
        Minutes,
        Seconds,
        DDMM,
        Custom
    }

    public class InvalidFormatterDataException : Exception
    {
        public InvalidFormatterDataException():this("The start date should not be greater than end date")
        {
        }

        public InvalidFormatterDataException(string message):base(message)
        {
        }

        public InvalidFormatterDataException(string message, Exception innerException):base(message, innerException)
        {
        }
    }

    public static class CustomTimeElapseProvider
    {
        public static string GetElapesedTimeString(this DateTime startDate, ElapsedTimeFormat format)
        {
            switch(format)
            {
                case ElapsedTimeFormat.Custom:
                    return CustomFormatter(startDate);

                default:
                    return CustomFormatter(startDate);
            }

        }

        private static string CustomFormatter(DateTime startDate)
        {
            if(startDate > DateTime.UtcNow)
            {
                throw new InvalidFormatterDataException();
            }

            var elapsedTime = (DateTime.UtcNow - startDate).Duration();

            if(elapsedTime.TotalSeconds < 60)
            {
                return (int)elapsedTime.TotalSeconds + (elapsedTime.TotalSeconds == 1 ? " sec ago" : " secs ago");
            }

            if (elapsedTime.TotalMinutes < 60)
            {
                return (int)elapsedTime.TotalMinutes + (elapsedTime.TotalMinutes == 1 ?" min ago" : " mins ago");
            }

            if(elapsedTime.TotalHours < 24)
            {
                return (int)elapsedTime.TotalHours + (elapsedTime.TotalHours == 1 ? " hr ago": " hrs ago");
            }

            if(elapsedTime.TotalDays < 30)
            {
                return (int)elapsedTime.TotalDays + (elapsedTime.TotalDays == 1 ? " day ago" : " days ago");
            }

            if(elapsedTime.TotalDays < 365)
            {
                var months = elapsedTime.TotalDays / 30;
                return (int)months + (months == 1 ? " month ago" : " months ago");
            }

            var years = elapsedTime.TotalDays / 365;
            return (int)years + (years == 1 ? " year ago" : " years ago");
        }
    }
}
