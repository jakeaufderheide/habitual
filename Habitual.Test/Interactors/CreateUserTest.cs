using Habitual.Core.UseCases.Impl;
using NUnit.Framework;
using Moq;
using Habitual.Core.Executors;
using Habitual.Core.Repositories;
using Habitual.Core.UseCases;
using Habitual.Core.Entities;
using Habitual.Test.Threading;

namespace Habitual.Test.Interactors
{
    [TestFixture]
    public class CreateUserInteractorTests
    {
        private MainThread mainThread;
        Mock<Executor> mockExecutor;
        Mock<UserRepository> mockRepository;
        Mock<CreateUserInteractorCallback> mockCallback;

        [SetUp]
        public void SetUp()
        {
            mockExecutor = new Mock<Executor>();
            mockRepository = new Mock<UserRepository>();
            mockCallback = new Mock<CreateUserInteractorCallback>();
            mainThread = new TestMainThread();
            
        }

        [TestCase("Test", "Password")]
        public void CreateUser_ValidUserAndPassword_CallRepoAndCallbackSuccess(string username, string password)
        {
            CreateUserInteractorImpl interactor = new CreateUserInteractorImpl(mockExecutor.Object, mainThread, mockCallback.Object, mockRepository.Object, username, password);

            interactor.Run();

            mockRepository.Verify(r => r.Create(It.Is<User>(u => u.Username == username && u.Password == password)), Times.Once());
            mockCallback.Verify(c => c.OnUserCreated(It.Is<User>(u => u.Username == username && u.Password == password)), Times.Once);
        }

        [TestCase("", "Password")]
        [TestCase("Test", "")]
        [TestCase("", "")]
        [TestCase(null, null)]
        public void CreateUser_UserOrPasswordMissing_CallbackError(string username, string password)
        {
            CreateUserInteractorImpl interactor = new CreateUserInteractorImpl(mockExecutor.Object, mainThread, mockCallback.Object, mockRepository.Object, username, password);

            interactor.Run();

            mockCallback.Verify(c => c.OnError(It.Is<string>(s => true)), Times.Once);
        }
    }
}
