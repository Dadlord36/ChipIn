using System.Collections.Generic;
using DataModels.MatchModels;
using UnityEngine;
using ViewModels.UI.Elements.Icons;

namespace Controllers.SlotsSpinningControllers
{
    public class SlotSpinningController : LineEngineController
    {
        [SerializeField] private GameSlotIconView slotPrefab;

        public void StartAnimating()
        {
            for (int i = 0; i < MovementElements.Length; i++)
            {
                MovementElements[i].StartAnimating();
            }
        }

        public void StopAnimating()
        {
            for (int i = 0; i < MovementElements.Length; i++)
            {
                MovementElements[i].StopAnimating();
            }
        }

        public void PrepareItems(List<BoardIconData> animatedIconResource, float slotsSpritesAnimationSwitchingInterval,
            bool loopTheAnimation)
        {
            var elementsTransforms = LineEngineBehaviour.Initialize(slotPrefab.transform, (uint) animatedIconResource.Count);
            MovementElements = new GameSlotIconView[elementsTransforms.Length];

            for (var i = 0; i < elementsTransforms.Length; i++)
            {
                MovementElements[i] = elementsTransforms[i].GetComponent<GameSlotIconView>();
            }

            CreateCorrespondingIndexesDictionary(animatedIconResource);

            for (int i = 0; i < MovementElements.Length; i++)
            {
                MovementElements[i].InitializeAnimator(animatedIconResource[i].AnimatedIconResource,
                    slotsSpritesAnimationSwitchingInterval, loopTheAnimation);
            }
        }
    }
}