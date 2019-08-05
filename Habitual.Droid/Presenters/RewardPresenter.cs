using System.Collections.Generic;
using Habitual.Core.Entities;
using Habitual.Droid.UI;

namespace Habitual.Droid.Presenters
{
    public interface RewardView : BaseView
    {
        void OnRewardsRetrieved(List<Reward> rewards);
        void OnRewardCreated(Reward reward);
        void OnRewardDeleted();
        void OnRewardBought(Reward reward);
        void OnError(string message);
    }

    public interface RewardPresenter : BasePresenter
    {
        void GetRewards(string username);
        void CreateReward(Reward reward);
        void DeleteReward(Reward reward);
        void BuyReward(Reward reward);
    }
}