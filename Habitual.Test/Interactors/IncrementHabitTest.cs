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
    public class IncrementHabitTest
    {

        private MainThread mainThread;
        private Mock<Executor> mockExecutor;
        private Mock<UserRepository> mockUserRepository;
        private Mock<HabitRepository> mockHabitRepository;
        private Mock<IncrementHabitInteractorCallback> mockCallback;
        private Mock<Habit> mockHabit;

        [SetUp]
        public void SetUp()
        {
            mockExecutor = new Mock<Executor>();
            mockUserRepository = new Mock<UserRepository>();
            mockHabitRepository = new Mock<HabitRepository>();
            mockCallback = new Mock<IncrementHabitInteractorCallback>();
            mockHabit = new Mock<Habit>();
            mainThread = new TestMainThread();
        }

        [TestCase(1,2)]
        public void IncrementHabit_ValidHabit_CallRepoAndCallbackSuccess(int count, int points)
        {
            mockHabitRepository.Setup(hr => hr.IncrementHabit(mockHabit.Object)).Returns(count);
            mockUserRepository.Setup(ur => ur.GetPoints(mockHabit.Object.Username)).Returns(points);

            IncrementHabitInteractorImpl interactor = new IncrementHabitInteractorImpl(mockExecutor.Object, mainThread, mockCallback.Object, mockHabitRepository.Object, mockUserRepository.Object, mockHabit.Object);
            interactor.Run();

            mockHabitRepository.Verify(hr => hr.IncrementHabit(mockHabit.Object));
            mockUserRepository.Verify(ur => ur.GetPoints(mockHabit.Object.Username));

            mockCallback.Verify(c => c.OnHabitIncremented(count, points));
        }


    }
}

