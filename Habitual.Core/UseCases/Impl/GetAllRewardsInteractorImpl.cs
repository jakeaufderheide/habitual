using System;
using Habitual.Core.Executors;
using Habitual.Core.Repositories;
using Habitual.Core.UseCases.Base;

namespace Habitual.Core.UseCases.Impl
{
    public class GetAllRewardsInteractorImpl : AbstractInteractor, GetAllRewardsInteractor
    {
        private GetAllRewardsInteractorCallback callback;
        private RewardRepository rewardRepository;
        private string username;

        public GetAllRewardsInteractorImpl(Executor taskExecutor, MainThread mainThread,
                                        GetAllRewardsInteractorCallback callback, RewardRepository rewardRepository,
                                        string username) : base(taskExecutor, mainThread)
        {
            this.callback = callback;
            this.rewardRepository = rewardRepository;
            this.username = username;
        }

        public async override void Run()
        {
            try
            {
                var rewards = await rewardRepository.GetAll(username);
                mainThread.Post(() => callback.OnRewardsRetrieved(rewards));
            } catch (Exception)
            {
                mainThread.Post(() => callback.OnError("Error getting rewards."));
            }
            
        }
    }
}
