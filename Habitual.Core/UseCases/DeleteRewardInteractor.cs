using System;
using Habitual.Core.UseCases.Base;

namespace Habitual.Core.UseCases
{
    public interface DeleteRewardInteractorCallback : InteractorCallback
	{
        void OnRewardDeleted(Guid rewardID);
	}
    public interface DeleteRewardInteractor : Interactor
    {
    }
}
