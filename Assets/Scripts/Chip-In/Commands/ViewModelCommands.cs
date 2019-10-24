namespace Commands
{
    /// <summary>
    /// Holds all commands of specific view model
    /// </summary>
    /// <typeparam name="T">View model type</typeparam>
    public abstract class ViewModelCommands<T>
    {
        abstract public void Initialize(T viewModel);
    }
}