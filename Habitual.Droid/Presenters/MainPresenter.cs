using Habitual.Core.Entities;
using Habitual.Droid.UI;

namespace Habitual.Droid.Presenters
{
    public interface MainView : BaseView
    {
        void OnUserCreated(User user);
        void OnUserRetrieved(User user);
        void OnUserStored(User user);
        void OnPointsRetrieved(int points);
        void OnAvatarSet(string imageString);
        void OnError(string message);
    }

    public interface MainPresenter : BasePresenter
    {
        void CreateUser(string username, string hashedPassword);
        void GetUser(string username, string hashedPassword);
        void StoreUserLocal(User user);
        void GetPoints(string username, string password);
        void SetAvatar(string username, string imageString);
    }
}