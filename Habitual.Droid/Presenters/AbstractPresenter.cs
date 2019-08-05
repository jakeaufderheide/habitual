using Habitual.Core.Executors;

namespace Habitual.Droid.Presenters
{
    public abstract class AbstractPresenter
    {
        protected Executor executor;
        protected MainThread mainThread;

        public AbstractPresenter(Executor executor, MainThread mainThread)
        {
            this.executor = executor;
            this.mainThread = mainThread;
        }
    }
}