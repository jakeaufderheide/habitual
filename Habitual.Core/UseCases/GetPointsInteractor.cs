using Habitual.Core.UseCases.Base;

namespace Habitual.Core.UseCases
{
    public interface GetPointsInteractorCallback : InteractorCallback
    {
        void OnPointsRetrieved(int points);
    }

    public interface GetPointsInteractor : Interactor
    {
    }
}
