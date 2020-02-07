using UnityEngine;

namespace Repositories.Local
{
    [CreateAssetMenu(fileName = nameof(SelectedGameRepository),
        menuName = nameof(Repositories) + "/" + nameof(SelectedGameRepository), order = 0)]
    public class SelectedGameRepository : ScriptableObject
    {
        public int GameId { get; set; }
    }
}