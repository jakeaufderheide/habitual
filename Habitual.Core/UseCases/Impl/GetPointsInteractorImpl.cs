using System;
using Habitual.Core.Executors;
using Habitual.Core.Repositories;
using Habitual.Core.UseCases.Base;

namespace Habitual.Core.UseCases.Impl
{
    public class GetPointsInteractorImpl : AbstractInteractor, GetPointsInteractor
    {
        private GetPointsInteractorCallback callback;
        private UserRepository userRepository;

        string username;
        string password;

        public GetPointsInteractorImpl(Executor taskExecutor, MainThread mainThread,
                                        GetPointsInteractorCallback callback, UserRepository userRepository,
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
                var points = await userRepository.GetPoints(username, password);

                mainThread.Post(() => callback.OnPointsRetrieved(points));
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
