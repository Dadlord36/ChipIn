using ScriptableObjects.DataSets;
using UnityEngine;

namespace Repositories.Remote
{
    [CreateAssetMenu(fileName = nameof(SlotGameIconsSetRepository),
        menuName = nameof(Repositories) + "/" + nameof(Remote) + "/" + nameof(SlotGameIconsSetRepository), order = 0)]
    public sealed class SlotGameIconsSetRepository : ScriptableObject
    {
        [SerializeField] private SlotGameIconsSetScriptableObject defaultSlotIconsSet;

        public IndexedSprite[] SlotsIcons => defaultSlotIconsSet.SlotGameIconsSet;
    }
}