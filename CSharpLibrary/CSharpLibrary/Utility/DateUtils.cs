using CSharpLibrary.Modals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpLibrary.Utility
{
    public class DateUtils
    {
        public static int CalculateWorkingDays(DateTime d1, DateTime d2)
        {
            DateTime startDate;
            DateTime endDate;
            if(d1 > d2)
            {
                startDate = d2;
                endDate = d1;
            }
            else
            {
                startDate = d1;
                endDate = d2;
            }

            var startDay = startDate.DayOfWeek;
            var daysDiff = (endDate - startDate).Days + 1; 
           
            if(daysDiff < 9)
            {
                var dayCount = 0;
                for(var i = 0; i < daysDiff; i++)
                {
                    var dayOfWeek = startDate.AddDays(i).DayOfWeek;
                    if(dayOfWeek != DayOfWeek.Saturday && dayOfWeek != DayOfWeek.Sunday)
                    {
                        dayCount++;
                    }
                }

                return dayCount;
            }

            var initialCutoff = GetInitialCutOff(startDay);
            var modifiedInitialDate = startDate.AddDays(initialCutoff);
           
            var totalDiffDays = (endDate - modifiedInitialDate).Days + 1;
            var weeks = totalDiffDays / 7;
            var finalCutoff = totalDiffDays % 7;
            if(finalCutoff > 5)
            {
                finalCutoff = 5;
            }

            if(!(startDay == DayOfWeek.Sunday || startDay == DayOfWeek.Saturday))
            {
                initialCutoff -= 2;
            }
            else
            {
                initialCutoff = 0;
            }

            return initialCutoff + (weeks * 5) + finalCutoff;
        }

        public static int CalculateWorkingDays(List<StartEndDatePair> pairs)
        {
            var allDates = new List<DateTime>();
            foreach(var pair in pairs)
            {
                var diff = Math.Abs((pair.EndDate - pair.StartDate).Days);
                for(var i =0; i <= diff; i++)
                {
                    allDates.Add(pair.StartDate.AddDays(i));
                }
            }

            var distDates = allDates.Distinct().ToList();

            var count = 0;
            foreach(var date in distDates)
            {
                var day = date.DayOfWeek;
                if(day != DayOfWeek.Sunday && day != DayOfWeek.Saturday)
                {
                    count++;
                }
            }

            return count;
        }

        private static int GetInitialCutOff(DayOfWeek day)
        {
            switch(day)
            {
                case DayOfWeek.Sunday: return 1;
                case DayOfWeek.Saturday: return 2;
                case DayOfWeek.Friday: return 3;
                case DayOfWeek.Thursday: return 4;
                case DayOfWeek.Wednesday: return 5;
                case DayOfWeek.Tuesday: return 6;
                case DayOfWeek.Monday: return 7;
                default: return 0;
            }
        }
    }
}
