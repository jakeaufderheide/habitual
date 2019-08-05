using System.Threading.Tasks;
using Habitual.Core.Entities;
using Habitual.Storage.DB.Base;
using Newtonsoft.Json;

namespace Habitual.Storage.DB
{
    public class UserDB :BaseDB
    {
        public async Task CreateUser(User user)
        {
            var jsonData = JsonConvert.SerializeObject(user);
            var jsonResult = await PostDataAsync("api/user/create", jsonData);
            return;
        }

        public async Task<User> GetUser(string username, string password)
        {
            var user = new User();
            user.Username = username;
            user.Password = password;
            var jsonRequest = JsonConvert.SerializeObject(user);
            var json = await PostDataAsync("api/user/get/", jsonRequest);
            var newUser = JsonConvert.DeserializeObject<User>(json);
            return newUser;
        }

        public async Task IncrementPoints(string username, int pointsToIncrement)
        {
            var jsonResult = await GetDataAsync($"api/user/points/{username}/{pointsToIncrement}");
            return;
        }
    }
}
