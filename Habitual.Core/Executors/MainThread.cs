using System;

namespace Habitual.Core.Executors
{
    public interface MainThread
    {
        /// <summary>
        /// Post an action with one param to the main thread (used for UI updates)
        /// </summary>
        void Post(Action action);

    }
}
