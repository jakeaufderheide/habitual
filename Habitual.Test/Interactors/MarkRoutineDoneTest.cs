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
    public class MarkRoutineDoneTest
    {
        private MainThread mainThread;
        private Mock<Executor> mockExecutor;
        private Mock<UserRepository> mockUserRepository;
        private Mock<RoutineRepository> mockRoutineRepository;
        private Mock<MarkRoutineDoneInteractorCallback> mockCallback;
        private Mock<Routine> mockRoutine;
        private Mock<Routine> mockUpdatedRoutine;
        private DateTime today;
        private DateTime yesterday;

        [SetUp]
        public void SetUp()
        {
            mockExecutor = new Mock<Executor>();
            mockUserRepository = new Mock<UserRepository>();
            mockRoutineRepository = new Mock<RoutineRepository>();
            mockCallback = new Mock<MarkRoutineDoneInteractorCallback>();
            mockRoutine = new Mock<Routine>();
            mockUpdatedRoutine = new Mock<Routine>();
            mainThread = new TestMainThread();
            today = new DateTime(2010, 12, 24);
            yesterday = today.AddDays(-1);


            //mockRoutine.Object.Logs = new List<RoutineLog>() { CreateFakeRoutineLog(yesterday)};
            //mockUpdatedRoutine.Object.Logs = new List<RoutineLog>();
            //mockUpdatedRoutine.Object.Logs.AddRange(mockRoutine.Object.Logs);
            //mockUpdatedRoutine.Object.Logs.Add(CreateFakeRoutineLog(today));
        }

        private RoutineLog CreateFakeRoutineLog(DateTime day)
        {
            var log = new RoutineLog();
            log.TimestampUTC = day;
            return log;
        }

        //[TestCase(100)]
        //public void MarkRoutineDone_ValidUndoneRoutine_CallRepoAndCallbackTodo(int newPoints)
        //{
        //    mockRoutineRepository.Setup(rr => rr.MarkDone(mockRoutine.Object)).Returns(mockUpdatedRoutine.Object);
        //    mockUserRepository.Setup(ur => ur.GetPoints(mockRoutine.Object.Username)).Returns(newPoints);

        //    MarkRoutineDoneInteractorImpl interactor = new MarkRoutineDoneInteractorImpl(mockExecutor.Object, mainThread, mockCallback.Object, mockUserRepository.Object, mockRoutineRepository.Object, mockRoutine.Object);
        //    interactor.Run();

        //    mockRoutineRepository.Verify(rr => rr.MarkDone(mockRoutine.Object), Times.Once);
        //    mockUserRepository.Verify(ur => ur.GetPoints(mockRoutine.Object.Username), Times.Once);

        //    mockCallback.Verify(c => c.OnRoutineMarkedDoneForToday(mockUpdatedRoutine.Object, newPoints));
        //}

        //public void MarkRoutineDone_RoutineLogNotAdded_CallbackError()
        //{
        //    mockRoutineRepository.Setup(rr => rr.MarkDone(mockRoutine.Object)).Returns(mockRoutine.Object);

        //    MarkRoutineDoneInteractorImpl interactor = new MarkRoutineDoneInteractorImpl(mockExecutor.Object, mainThread, mockCallback.Object, mockUserRepository.Object, mockRoutineRepository.Object, mockRoutine.Object);
        //    interactor.Run();

        //    mockCallback.Verify(c => c.OnError(It.Is<string>(s => true)));
        //}
    }
}
