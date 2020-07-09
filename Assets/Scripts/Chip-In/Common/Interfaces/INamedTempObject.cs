namespace Common.Interfaces
{
    public interface INamedTempObject
    {
        string Name { get; }
        void Destroy();
    }
}