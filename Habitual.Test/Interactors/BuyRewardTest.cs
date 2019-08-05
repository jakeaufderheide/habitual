using Habitual.Core.Entities;
using Habitual.Core.Executors;
using Habitual.Core.Repositories;
using Habitual.Core.UseCases;
using Habitual.Core.UseCases.Impl;
using Habitual.Test.Threading;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Habitual.Test.Interactors
{
    [TestFixture]
    public class BuyRewardTest
    {
        private MainThread mainThread;
        private Mock<Executor> mockExecutor;
        private Mock<UserRepository> mockUserRepository;
        private Mock<RewardRepository> mockRewardRepository;
        private Mock<BuyRewardInteractorCallback> mockCallback;
        private Mock<Reward> mockReward;

        [SetUp]
        public void SetUp()
        {
            mockExecutor = new Mock<Executor>();
            mockUserRepository = new Mock<UserRepository>();
            mockRewardRepository = new Mock<RewardRepository>();
            mockCallback = new Mock<BuyRewardInteractorCallback>();
            mockReward = new Mock<Reward>();
            mainThread = new TestMainThread();
        }

        [TestCase(100)]
        public void BuyReward_PurchaseSuccess_CallRepoAndCallbackNewPointValue(int points)
        {
            mockRewardRepository.Setup(rr => rr.BuyReward(mockReward.Object)).Returns(true);
            mockUserRepository.Setup(ur => ur.GetPoints(mockReward.Object.Username)).Returns(points);

            BuyRewardInteractorImpl interactor = new BuyRewardInteractorImpl(mockExecutor.Object, mainThread, mockCallback.Object, mockRewardRepository.Object, mockUserRepository.Object, mockReward.Object);
            interactor.Run();

            mockRewardRepository.Verify(rr => rr.BuyReward(mockReward.Object), Times.Once);
            mockUserRepository.Verify(ur => ur.GetPoints(mockReward.Object.Username), Times.Once);

            mockCallback.Verify(c => c.OnRewardPurchased(mockReward.Object, points));
        }

        [Test]
        public void BuyReward_PurchaseFailed_CallbackError()
        {
            mockRewardRepository.Setup(rr => rr.BuyReward(mockReward.Object)).Returns(false);

            BuyRewardInteractorImpl interactor = new BuyRewardInteractorImpl(mockExecutor.Object, mainThread, mockCallback.Object, mockRewardRepository.Object, mockUserRepository.Object, mockReward.Object);
            interactor.Run();

            mockRewardRepository.Verify(rr => rr.BuyReward(mockReward.Object), Times.Once);
            mockCallback.Verify(c => c.OnError(It.Is<string>(s => true)), Times.Once);
        }
    }
}
