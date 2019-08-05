using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Habitual.Core.Entities;
using Habitual.Core.Repositories;
using Habitual.Storage.DB;

namespace Habitual.Storage
{
    public class RewardRepositoryImpl : RewardRepository
    {

        public async Task Create(Reward reward)
        {
            RewardDB rewardManager = new RewardDB();
            await rewardManager.CreateReward(reward);
            return;
        }

        public async Task Delete(Guid id)
        {
            RewardDB rewardManager = new RewardDB();
            await rewardManager.DeleteReward(id);
            return;
        }

        public async Task<List<Reward>> GetAll(string username)
        {
            RewardDB rewardManager = new RewardDB();
            var rewards = await rewardManager.GetAllRewards(username);
            return rewards;
        }
    }
}
