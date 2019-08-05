using System.Collections.Generic;

namespace Habitual.Core.Entities
{
    public class TaskContainer
    {
        private TaskContainer()
        {

        }
        public static TaskContainer GetTaskContainer()
        {
            var container = new TaskContainer();
            container.Habits = new List<Habit>();
            container.Routines = new List<Routine>();
            container.Todos = new List<Todo>();
            container.HabitLogs = new List<HabitLog>();
            container.RoutineLogs = new List<RoutineLog>();
            return container;
        }
        public List<Habit> Habits { get; set; }
        public List<Routine> Routines { get; set; }
        public List<Todo> Todos { get; set; }
        public List<HabitLog> HabitLogs { get; set; }
        public List<RoutineLog> RoutineLogs { get; set; }
    }
}
