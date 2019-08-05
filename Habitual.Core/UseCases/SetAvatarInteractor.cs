using Habitual.Core.UseCases.Base;

namespace Habitual.Core.UseCases
{
    public interface SetAvatarInteractorCallback : InteractorCallback
    {
        void OnAvatarSet(string imageString);
    }
    public interface SetAvatarInteractor : Interactor
    {
    }
}
