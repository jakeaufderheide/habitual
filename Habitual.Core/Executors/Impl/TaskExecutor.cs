using System.Threading.Tasks;
using Habitual.Core.UseCases.Base;

namespace Habitual.Core.Executors.Impl
{
    public class TaskExecutor : Executor
    {
        private static volatile TaskExecutor taskExecutorSingleton;

        public void Execute(AbstractInteractor interactor)
        {
            // starts interactor on new thread using Task API
            Task.Run(() =>
            {
                interactor.Run();
                interactor.OnFinished();
            });
            
        }

        public static Executor GetInstance()
        {
            if (taskExecutorSingleton == null)
            {
                taskExecutorSingleton = new TaskExecutor();
            }

            return taskExecutorSingleton;
        }
    }
}
