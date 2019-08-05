using System;

namespace Habitual.Core.Entities
{
    public class RoutineLog
    {
        public Guid ID { get; set; }
        public Guid RoutineID { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
