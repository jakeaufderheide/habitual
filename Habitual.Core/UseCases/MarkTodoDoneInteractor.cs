using Habitual.Core.Entities;
using Habitual.Core.UseCases.Base;

namespace Habitual.Core.UseCases
{
    public interface MarkTodoDoneInteractorCallback : InteractorCallback
	{
        void OnTodoMarkedDone(Todo todo, int pointsAdded);
	}
    public interface MarkTodoDoneInteractor : Interactor
    {
    }
}
