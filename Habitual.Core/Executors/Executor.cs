using Habitual.Core.UseCases.Base;

namespace Habitual.Core.Executors
{
    public interface Executor
    {
        void Execute(AbstractInteractor interactor);
    }
}
