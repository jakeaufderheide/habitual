using Habitual.Core.Entities;
using Habitual.Core.UseCases.Base;

namespace Habitual.Core.UseCases
{
    public interface GetUserInteractorCallback : InteractorCallback
    {
        void OnUserRetrieved(User user);
    }

    public interface GetUserInteractor : Interactor
    {

    }
}