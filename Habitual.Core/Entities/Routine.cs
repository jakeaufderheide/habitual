using System;
using System.Collections.Generic;
using Habitual.Core.Entities.Base;

namespace Habitual.Core.Entities
{
    public class Routine : BaseTask
    {
        public bool IsActiveSunday { get; set; }
        public bool IsActiveMonday { get; set; }
        public bool IsActiveTuesday { get; set; }
        public bool IsActiveWednesday { get; set; }
        public bool IsActiveThursday { get; set; }
        public bool IsActiveFriday { get; set; }
        public bool IsActiveSaturday { get; set; }

        public List<DayOfWeek> DaysActive()
        {
            var daysOfWeek = new List<DayOfWeek>();
            if (IsActiveSunday) daysOfWeek.Add(DayOfWeek.Sunday);
            if (IsActiveMonday) daysOfWeek.Add(DayOfWeek.Monday);
            if (IsActiveTuesday) daysOfWeek.Add(DayOfWeek.Tuesday);
            if (IsActiveWednesday) daysOfWeek.Add(DayOfWeek.Wednesday);
            if (IsActiveThursday) daysOfWeek.Add(DayOfWeek.Thursday);
            if (IsActiveFriday) daysOfWeek.Add(DayOfWeek.Friday);
            if (IsActiveSaturday) daysOfWeek.Add(DayOfWeek.Sunday);
            return daysOfWeek;
        }
        public bool IsActiveThisDay(DayOfWeek day)
        {
            if (IsActiveSunday && day == DayOfWeek.Sunday) return true;
            if (IsActiveMonday && day == DayOfWeek.Monday) return true;
            if (IsActiveTuesday && day == DayOfWeek.Tuesday) return true;
            if (IsActiveWednesday && day == DayOfWeek.Wednesday) return true;
            if (IsActiveThursday && day == DayOfWeek.Thursday) return true;
            if (IsActiveFriday && day == DayOfWeek.Friday) return true;
            if (IsActiveSaturday && day == DayOfWeek.Friday) return true;
            return false;
        }
    }
}
