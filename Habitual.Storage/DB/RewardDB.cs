using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Habitual.Core.Entities;
using Habitual.Storage.DB.Base;
using Newtonsoft.Json;

namespace Habitual.Storage.DB
{
    public class RewardDB : BaseDB
    {
        public async Task CreateReward(Reward reward)
        {
            var jsonData = JsonConvert.SerializeObject(reward);
            var jsonResult = await PostDataAsync("api/reward/create", jsonData);
            return;
        }

        public async Task<List<Reward>> GetAllRewards(string username)
        {
            var jsonResult = await GetDataAsync($"api/reward/getall/{username}");
            var rewards = JsonConvert.DeserializeObject<List<Reward>>(jsonResult);
            return rewards;
        }

        public async Task DeleteReward(Guid id)
        {
            var jsonResult = await GetDataAsync($"api/reward/delete/{id.ToString()}");
            return;
        }

        public async Task<bool> BuyReward(Reward reward)
        {
            var jsonData = JsonConvert.SerializeObject(reward);
            var jsonResult = await PostDataAsync("api/reward/buy", jsonData);
            var result = JsonConvert.DeserializeObject<bool>(jsonResult);
            return result;
        }
    }
}
