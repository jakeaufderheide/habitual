using System.Collections.Generic;
using Habitual.Core.Entities;
using Habitual.Core.UseCases.Base;

namespace Habitual.Core.UseCases
{
    public interface GetAllRewardsInteractorCallback : InteractorCallback
	{
        void OnRewardsRetrieved(List<Reward> rewards);
	}
    public interface GetAllRewardsInteractor : Interactor
    {
    }
}
