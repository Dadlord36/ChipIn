using System.Threading.Tasks;

namespace Common.Interfaces
{
    public interface IApplicationClosingEventReceiver
    {
        Task OnApplicationClosing();
    }
}