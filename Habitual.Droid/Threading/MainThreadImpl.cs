using System;
using Android.Support.V4.App;
using Habitual.Core.Executors;

namespace Habitual.Droid.Threading
{
    public class MainThreadImpl : MainThread
    {
        private FragmentActivity activity;

        public MainThreadImpl(FragmentActivity activity) : base()
        {
            this.activity = activity;
        }

        public void Post(Action action)
        {
            activity.RunOnUiThread(action);
        }

        public void Post<T>(Action<T> action)
        {
            throw new NotImplementedException();
        }
    }
}