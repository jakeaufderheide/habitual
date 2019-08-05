using Habitual.Core.Entities;
using Habitual.Core.UseCases.Base;

namespace Habitual.Core.UseCases
{
    public interface StoreUserLocalCallback : InteractorCallback
    {
        void OnUserStored(User user);
    }

    public interface StoreUserLocalInteractor : Interactor
    {
    }
}
