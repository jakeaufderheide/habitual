using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Habitual.Core.Entities;
using Habitual.Core.Repositories;
using Habitual.Storage.DB;
using Habitual.Storage.Local;
using Newtonsoft.Json;

namespace Habitual.Storage
{
    public class UserRepositoryImpl : UserRepository
    {
        public async Task Create(User user)
        {
            UserDB userManager = new UserDB();
            await userManager.CreateUser(user);
            return;
        }

        //Not used for User
        public Task Delete(Guid userID)
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetUser(string username, string password)
        {
            UserDB userManager = new UserDB();
            var user = await userManager.GetUser(username, password);
            user.Avatar = GetAvatarLocal(username);
            return user;
        }

        private byte[] GetAvatarLocal(string username)
        {
            Dictionary<string, string> avatarStorage;
            var imageString = "";

            if (string.IsNullOrEmpty(LocalData.AvatarStorage))
            {
                avatarStorage = new Dictionary<string, string>();
            }
            else
            {
                avatarStorage = JsonConvert.DeserializeObject<Dictionary<string, string>>(LocalData.AvatarStorage);
            }

            if (avatarStorage.ContainsKey(username))
            {
                imageString = avatarStorage[username];
                return Convert.FromBase64String(imageString);
            }

            return null;
        }

        public async Task<int> GetPoints(string username, string password)
        {
            UserDB userManager = new UserDB();
            var user = await userManager.GetUser(username, password);
            return user.Points;
        }

        public async Task IncrementPoints(string username, int pointsToIncrement)
        {
            UserDB userManager = new UserDB();
            await userManager.IncrementPoints(username, pointsToIncrement);
            return;
        }

        public void StoreLocally(User user)
        {
            LocalData.Username = user.Username;
            LocalData.Password = user.Password;
        }

        public async Task<bool> BuyReward(Reward reward)
        {
            RewardDB rewardManager = new RewardDB();
            var result = await rewardManager.BuyReward(reward);
            return result;
        }

        //done locally for sake of example - faster and less likely to use up my trial db space or bandwidth
        public void SetAvatar(string username, string imageString)
        {
            Dictionary<string, string> avatarStorage;
            if (string.IsNullOrEmpty(LocalData.AvatarStorage))
            {
                avatarStorage = new Dictionary<string, string>();
            } else
            {
                avatarStorage = JsonConvert.DeserializeObject<Dictionary<string,string>>(LocalData.AvatarStorage);
            }
            
            if (avatarStorage.ContainsKey(username))
            {
                avatarStorage[username] = imageString;
            } else
            {
                avatarStorage.Add(username, imageString);
            }
            LocalData.AvatarStorage = JsonConvert.SerializeObject(avatarStorage);
        }

        //Not used for User
        public Task<List<User>> GetAll(string username)
        {
            throw new NotImplementedException();
        }
    }
}
