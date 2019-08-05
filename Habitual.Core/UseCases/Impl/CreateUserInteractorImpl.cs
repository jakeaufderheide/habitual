using System;
using Habitual.Core.Entities;
using Habitual.Core.Executors;
using Habitual.Core.Repositories;
using Habitual.Core.UseCases.Base;

namespace Habitual.Core.UseCases.Impl
{
    public class CreateUserInteractorImpl : AbstractInteractor, CreateUserInteractor
    {
        private CreateUserInteractorCallback callback;
        private UserRepository userRepository;

        private string username;
        private string password;

        public CreateUserInteractorImpl(Executor taskExecutor, MainThread mainThread,
                                        CreateUserInteractorCallback callback, UserRepository userRepository,
                                        string username, string password) : base(taskExecutor, mainThread)
        {
            this.callback = callback;
            this.userRepository = userRepository;
            this.username = username;
            this.password = password;
        }

        public async override void Run()
        {
            try
            {
                if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(username))
                {
                    mainThread.Post(() => { callback.OnError("Password cannot be blank"); });
                    return;
                }

                User user = new User(username, password);
                var alreadyExistingUser = await userRepository.GetUser(username, password);

                if (alreadyExistingUser == null || alreadyExistingUser.Username == null)
                {
                    await userRepository.Create(user);

                    mainThread.Post(() => { callback.OnUserCreated(user); });
                }
            } catch (Exception)
            {
                callback.OnError("Error creating user. User may already exist.");
            }
            
            
        }
    }
}
