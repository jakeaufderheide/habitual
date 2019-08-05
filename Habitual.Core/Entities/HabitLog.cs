using System;

namespace Habitual.Core.Entities
{
    public class HabitLog
    {
        public Guid ID { get; set; }
        public Guid HabitID { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
