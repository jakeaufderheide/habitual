using Habitual.Core.Entities;
using Habitual.Core.Executors;
using Habitual.Core.Repositories;
using Habitual.Core.UseCases;
using Habitual.Core.UseCases.Impl;

namespace Habitual.Droid.Presenters.Impl
{
    public class MainPresenterImpl : AbstractPresenter, MainPresenter, CreateUserInteractorCallback, GetUserInteractorCallback, StoreUserLocalCallback, GetPointsInteractorCallback, SetAvatarInteractorCallback
    {
        private MainView view;
        private UserRepository userRepository;

        public MainPresenterImpl(Executor executor, MainThread mainThread, MainView view,
                                 UserRepository userRepository) : base(executor, mainThread)
        {
            this.view = view;
            this.userRepository = userRepository;
        }

        public void GetUser(string username, string password)
        {
            GetUserInteractor getUserInteractor = new GetUserInteractorImpl(executor, mainThread, this, userRepository, username, password);
            getUserInteractor.Execute();
        }

        public void CreateUser(string username, string password)
        {
            CreateUserInteractor createUserInteractor = new CreateUserInteractorImpl(executor, mainThread, this, userRepository, username, password);
            createUserInteractor.Execute();
        }

        public void StoreUserLocal(User user)
        {
            StoreUserLocalInteractor storeUserInteractor = new StoreUserLocalInteractorImpl(executor, mainThread, this, userRepository, user);
            storeUserInteractor.Execute();
        }

        public void Destroy()
        {
            
        }

        public void OnError(string message)
        {
            view.OnError(message); 
        }

        public void Pause()
        {
            
        }

        public void Resume()
        {
            
        }

        public void Stop()
        {
            
        }

        public void OnUserCreated(User user)
        {
            view.OnUserCreated(user);
        }

        public void OnInteractorError(string message)
        {
            OnError(message);
        }

        public void OnUserRetrieved(User user)
        {
            view.OnUserRetrieved(user);
        }

        public void OnUserStored(User user)
        {
            view.OnUserStored(user);
        }

        public void GetPoints(string username, string password)
        {
            GetPointsInteractor interactor = new GetPointsInteractorImpl(executor, mainThread, this, userRepository, username, password);
            interactor.Execute();
        }

        public void OnPointsRetrieved(int points)
        {
            view.OnPointsRetrieved(points);
        }

        public void SetAvatar(string username, string imageString)
        {
            SetAvatarInteractor interactor = new SetAvatarInteractorImpl(executor, mainThread, this, userRepository, username, imageString);
            interactor.Execute();
        }

        public void OnAvatarSet(string imageString)
        {
            view.OnAvatarSet(imageString);
        }
    }
}