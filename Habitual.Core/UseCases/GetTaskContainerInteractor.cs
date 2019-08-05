using Habitual.Core.Entities;
using Habitual.Core.UseCases.Base;

namespace Habitual.Core.UseCases
{
    public interface GetTaskContainerCallback : InteractorCallback
    {
        void OnTaskContainerFilled(TaskContainer taskContainer);
    }
    public interface GetTaskContainerInteractor : Interactor
    {
    }
}
