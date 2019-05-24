using LibraryData.Models;
using System;
using System.Collections.Generic;

namespace LibraryServices
{
   public class DataHelpers
    {
        public static List<string> HumanizeBizHours(IEnumerable<BranchHours> branchHours)
        {
            var hours = new List<string>();
            foreach (var time in branchHours)
            {
                var day = HumanizeDay(time.DaysOfWeek);
                var opentime = HumanizeTime(time.OpenTime);
                var closeTime = HumanizeTime(time.CloseTime);

                var timeEntry = $"{day} {opentime} to {closeTime}";
                hours.Add(timeEntry);
            }
            return hours;
        }

        public static string HumanizeTime(int time)
        {
            return TimeSpan.FromHours(time).ToString("hh':'mm");
        }

        public static string HumanizeDay(int daysOfWeek)
        {
            // One to Sunday 
            return Enum.GetName(typeof(DayOfWeek), daysOfWeek -1);
        }
    }
}
