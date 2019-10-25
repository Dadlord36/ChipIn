namespace ViewModels.Interfaces
{
    public interface IRegistrationListener
    {
        void RequestStarted();
        void RequestSuccessful();
        void RequestFailure(string message);
    }
}