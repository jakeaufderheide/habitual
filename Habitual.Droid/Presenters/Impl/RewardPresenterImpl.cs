using System;
using System.Collections.Generic;
using Habitual.Core.Entities;
using Habitual.Core.Executors;
using Habitual.Core.Repositories;
using Habitual.Core.UseCases;
using Habitual.Core.UseCases.Impl;

namespace Habitual.Droid.Presenters.Impl
{
    public class RewardPresenterImpl : AbstractPresenter, RewardPresenter, GetAllRewardsInteractorCallback, CreateRewardInteractorCallback, DeleteRewardInteractorCallback, BuyRewardInteractorCallback
    {
        private RewardView view;
        private RewardRepository rewardRepository;
        private UserRepository userRepository;
        private string username;
        private string password;

        public RewardPresenterImpl(Executor executor, MainThread mainThread, RewardView view, RewardRepository rewardRepository, UserRepository userRepository, string username, string password) : base(executor, mainThread)
        {
            this.view = view;
            this.rewardRepository = rewardRepository;
            this.userRepository = userRepository;
            this.username = username;
            this.password = password;
        }

        public void Resume()
        {
            throw new NotImplementedException();
        }

        public void Pause()
        {
            throw new NotImplementedException();
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }

        public void Destroy()
        {
            throw new NotImplementedException();
        }

        public void OnError(string message)
        {
            view.OnError(message);
        }

        public void GetRewards(string username)
        {
            GetAllRewardsInteractor interactor = new GetAllRewardsInteractorImpl(executor, mainThread, this, rewardRepository, username);
            interactor.Execute();
        }

        public void OnRewardsRetrieved(List<Reward> rewards)
        {
            view.OnRewardsRetrieved(rewards);
        }

        public void CreateReward(Reward reward)
        {
            CreateRewardInteractor interactor = new CreateRewardInteractorImpl(executor, mainThread, this, rewardRepository, reward, username);
            interactor.Execute();
        }

        public void OnRewardCreated(Reward reward)
        {
            view.OnRewardCreated(reward);
        }

        public void DeleteReward(Reward reward)
        {
            DeleteRewardInteractor interactor = new DeleteRewardInteractorImpl(executor, mainThread, this, rewardRepository, reward);
            interactor.Execute();
        }

        public void OnRewardDeleted(Guid rewardID)
        {
            view.OnRewardDeleted();
        }

        public void BuyReward(Reward reward)
        {
            BuyRewardInteractor interactor = new BuyRewardInteractorImpl(executor, mainThread, this, rewardRepository, userRepository, reward);
            interactor.Execute();
        }

        public void OnRewardPurchased(Reward reward, int newPoints)
        {
            view.OnRewardBought(reward);
        }
    }
}