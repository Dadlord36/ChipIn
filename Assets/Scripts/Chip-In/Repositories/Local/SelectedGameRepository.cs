using UnityEngine;
using Utilities;

namespace Repositories.Local
{
    [CreateAssetMenu(fileName = nameof(SelectedGameRepository),
        menuName = nameof(Repositories) + "/" + nameof(Local) + "/" + nameof(SelectedGameRepository), order = 0)]
    public class SelectedGameRepository : ScriptableObject
    {
        private const string Tag = nameof(SelectedGameRepository);

        private int _selectedGameId;

        public int GameId
        {
            get => _selectedGameId;
            set
            {
                _selectedGameId = value;
                LogUtility.PrintLog(Tag, $"Selected game ID was changed to: {value.ToString()}");
            }
        }
    }
}