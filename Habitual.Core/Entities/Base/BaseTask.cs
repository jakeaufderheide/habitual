/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
 * Jacob Aufderheide
 * North Central College - Master's Project Application
 * Primary Reader: Brian Craig
 * Xamarin Cross-Platform Habit Tracking Application
 * 5/15/2017
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

using System;

namespace Habitual.Core.Entities.Base
{
    public abstract class BaseTask
    {
        public Guid ID { get; set; }
        public string Username { get; set; }
        public string Description { get; set; }
        public Difficulty Difficulty { get; set; }
    }
}
