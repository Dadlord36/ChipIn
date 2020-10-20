using UnityEngine;
using ViewModels;

namespace Repositories.Local
{
    public interface IChatBotsRepository
    {
        ChatBotParametersData GetRandomBotData();
    }

    [CreateAssetMenu(fileName = nameof(ChatBotsRepository), menuName = nameof(Repositories) + "/" + nameof(Local) +
                                                                       "/" + nameof(ChatBotsRepository), order = 0)]
    public class ChatBotsRepository : ScriptableObject, IChatBotsRepository
    {
        [SerializeField] private ChatBotParametersData[] bots;

        public ChatBotParametersData GetRandomBotData()
        {
            return bots[Random.Range(0, bots.Length - 1)];
        }
    }
}