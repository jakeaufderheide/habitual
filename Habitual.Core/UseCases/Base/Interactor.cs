namespace Habitual.Core.UseCases.Base
{
    public interface InteractorCallback
    {
        void OnError(string message);
    }
    public interface Interactor
    {
        void Execute();
    }
}
