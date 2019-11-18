namespace Common
{
    public interface INamedTempObject
    {
        string Name { get; }
        void Destroy();
    }
}