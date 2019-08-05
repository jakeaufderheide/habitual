using Habitual.Core.Entities;
using Habitual.Core.UseCases.Base;

namespace Habitual.Core.UseCases
{
    public interface CreateRewardInteractorCallback : InteractorCallback
	{
        void OnRewardCreated(Reward reward);
	}
    public interface CreateRewardInteractor : Interactor
    {
    }
}
