using System;

namespace Habitual.Core.Entities
{
    public class Reward
    {
        public string Username { get; set; }
        public Guid ID { get; set; }
        public string Description { get; set; }
        public int Cost { get; set; }
    }
}
