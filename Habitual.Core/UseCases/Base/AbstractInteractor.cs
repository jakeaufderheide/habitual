using Habitual.Core.Executors;

namespace Habitual.Core.UseCases.Base
{
    public abstract class AbstractInteractor : Interactor
    {
        protected Executor taskExecutor;
        protected MainThread mainThread;

        protected volatile bool isRunning;
        protected volatile bool isCanceled;

        public bool IsRunning { get { return isRunning; } }

        public AbstractInteractor(Executor taskExecutor, MainThread mainThread)
        {
            this.taskExecutor = taskExecutor;
            this.mainThread = mainThread;
        }

        /// <summary>
        /// Should not be called directly. Called on background thread by executor.
        /// It's public so it can be called for unit testing
        /// </summary>
        public abstract void Run();

        public void Execute()
        {
            isRunning = true;

            taskExecutor.Execute(this);
        }

        public void Cancel()
        {
            isCanceled = true;
            isRunning = false;
        }

        public void OnFinished()
        {
            isRunning = false;
            isCanceled = false;
        }

    }
}
