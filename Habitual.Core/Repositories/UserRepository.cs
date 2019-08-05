using System.Threading.Tasks;
using Habitual.Core.Entities;

namespace Habitual.Core.Repositories
{
    public interface UserRepository : Repository<User>
    {
        Task<User> GetUser(string username, string password);
        Task<int> GetPoints(string username, string password);
        Task IncrementPoints(string username, int pointsToIncrement);
        void StoreLocally(User user);
        Task<bool> BuyReward(Reward reward);
        void SetAvatar(string username, string imageString);
    }
}
