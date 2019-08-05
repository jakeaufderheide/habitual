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
    public class MarkTodoDoneTest
    {
        private MainThread mainThread;
        private Mock<Executor> mockExecutor;
        private Mock<UserRepository> mockUserRepository;
        private Mock<TodoRepository> mockTodoRepository;
        private Mock<MarkTodoDoneInteractorCallback> mockCallback;
        private Mock<Todo> mockTodo;
        private Mock<Todo> mockDoneTodo;

        [SetUp]
        public void SetUp()
        {
            mockExecutor = new Mock<Executor>();
            mockUserRepository = new Mock<UserRepository>();
            mockTodoRepository = new Mock<TodoRepository>();
            mockCallback = new Mock<MarkTodoDoneInteractorCallback>();
            mockTodo = new Mock<Todo>();
            mockDoneTodo = new Mock<Todo>();
            mainThread = new TestMainThread();
        }

        [TestCase(100)]
        public void MarkTodoDone_ValidUnDoneTodo_CallRepoAndCallbackTodo(int newPoints)
        {
            mockTodo.Object.IsDone = false;
            mockDoneTodo.Object.IsDone = true;
            mockTodoRepository.Setup(tr => tr.MarkDone(mockTodo.Object)).Returns(mockDoneTodo.Object);
            mockUserRepository.Setup(ur => ur.GetPoints(mockTodo.Object.Username)).Returns(newPoints);

            MarkTodoDoneInteractorImpl interactor = new MarkTodoDoneInteractorImpl(mockExecutor.Object, mainThread, mockCallback.Object, mockUserRepository.Object, mockTodoRepository.Object, mockTodo.Object);
            interactor.Run();

            mockTodoRepository.Verify(tr => tr.MarkDone(mockTodo.Object), Times.Once);
            mockUserRepository.Verify(ur => ur.GetPoints(mockTodo.Object.Username), Times.Once);

            mockCallback.Verify(c => c.OnTodoMarkedDone(mockDoneTodo.Object, newPoints));
        }

        [Test]
        public void MarkTodoDone_TodoAlreadyDone_CallbackError()
        {
            mockTodo.Object.IsDone = true;
            MarkTodoDoneInteractorImpl interactor = new MarkTodoDoneInteractorImpl(mockExecutor.Object, mainThread, mockCallback.Object, mockUserRepository.Object, mockTodoRepository.Object, mockTodo.Object);
            interactor.Run();

            mockCallback.Verify(c => c.OnError(It.Is<string>(s => true)));
        }

        [Test]
        public void MarkTodoDone_ReturnedTodoIsNotDone_CallbackError()
        {
            mockTodo.Object.IsDone = false;
            mockDoneTodo.Object.IsDone = false;
            mockTodoRepository.Setup(tr => tr.MarkDone(mockTodo.Object)).Returns(mockDoneTodo.Object);

            MarkTodoDoneInteractorImpl interactor = new MarkTodoDoneInteractorImpl(mockExecutor.Object, mainThread, mockCallback.Object, mockUserRepository.Object, mockTodoRepository.Object, mockTodo.Object);
            interactor.Run();

            mockCallback.Verify(c => c.OnError(It.Is<string>(s => true)));
        }
    }
}
