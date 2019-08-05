
using System;
using Habitual.Core.Executors;
using Habitual.Core.Repositories;
using Habitual.Core.UseCases.Base;

namespace Habitual.Core.UseCases.Impl
{
    public class GetUserInteractorImpl : AbstractInteractor, GetUserInteractor
    {
        private GetUserInteractorCallback callback;
        private UserRepository userRepository;

        string username;
        string password;

        public GetUserInteractorImpl(Executor taskExecutor, MainThread mainThread,
                                        GetUserInteractorCallback callback, UserRepository userRepository,
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
                var user = await userRepository.GetUser(username, password);

                callback.OnUserRetrieved(user);
            }
            catch (Exception)
            {
                mainThread.Post(() => callback.OnError("Error gettting user."));
            }
        }
    }
}
