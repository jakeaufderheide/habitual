using Habitual.Core.Entities;
using Habitual.Core.UseCases.Base;

namespace Habitual.Core.UseCases
{
    public interface BuyRewardInteractorCallback : InteractorCallback
	{
        void OnRewardPurchased(Reward reward, int newPoints);
	}
    public interface BuyRewardInteractor : Interactor
    {
    }
}
