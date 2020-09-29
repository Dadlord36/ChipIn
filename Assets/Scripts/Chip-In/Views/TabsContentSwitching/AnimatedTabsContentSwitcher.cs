using System.Collections.Generic;
using CustomAnimators;
using CustomAnimators.GeneratedAnimationActions;
using UnityEngine;

namespace Views.TabsContentSwitching
{
    public class AnimatedTabsContentSwitcher : MonoBehaviour
    {
        [SerializeField] private Transform[] containers;
        [SerializeField] private CanvasGroupFading.AnimationParameters animationParameters;

        private readonly ProgressiveOperationsController _progressiveOperationsController = new ProgressiveOperationsController();

        private int _currentContainer;

        public int CurrentContainer
        {
            get => _currentContainer;
            set
            {
                if (_currentContainer == value) return;
                _currentContainer = value;
                SwitchToContainerByIndex(value);
            }
        }

        private void Start()
        {
            StopUpdate();
            _progressiveOperationsController.ProgressReachesEnd += StopUpdate;
        }

        private void StartUpdate()
        {
            enabled = true;
        }

        private void StopUpdate()
        {
            enabled = false;
        }

        private void SwitchToContainerByIndex(int index)
        {
            StopUpdate();
            ResetProgressiveOperationsController();

            GetCanvasGroups(index, out var tabCanvasGroup, out var otherCanvasGroups);
            
            tabCanvasGroup.transform.SetAsLastSibling();
            
            tabCanvasGroup.blocksRaycasts = true;
            tabCanvasGroup.interactable = true;

            foreach (var canvasGroup in otherCanvasGroups)
            {
                canvasGroup.blocksRaycasts = false;
                canvasGroup.interactable = false;
            }

            var fadingActions = PrepareFadingAnimations(new[] {tabCanvasGroup}, otherCanvasGroups);

            foreach (var action in fadingActions)
            {
                _progressiveOperationsController.AddProgressiveOperation(action);
            }

            StartUpdate();
        }

        private IEnumerable<ProgressiveAction> PrepareFadingAnimations(IReadOnlyList<CanvasGroup> fadingInGroups, IReadOnlyList<CanvasGroup> fadingOutGroups)
        {
            var canvasGroupFadings = new List<ProgressiveAction>(fadingInGroups.Count + fadingOutGroups.Count);
            canvasGroupFadings.AddRange(CreateFadingActions(fadingInGroups, CanvasGroupFading.FadingType.Appear));
            canvasGroupFadings.AddRange(CreateFadingActions(fadingOutGroups, CanvasGroupFading.FadingType.Disappear));
            return canvasGroupFadings;
        }

        private IEnumerable<ProgressiveAction> CreateFadingActions(IReadOnlyList<CanvasGroup> canvasGroups, CanvasGroupFading.FadingType fadingType)
        {
            var actions = new ProgressiveAction[canvasGroups.Count];
            for (int i = 0; i < canvasGroups.Count; i++)
            {
                actions[i] = new CanvasGroupFading(animationParameters, canvasGroups[i], fadingType);
            }

            return actions;
        }

        private void GetCanvasGroups(int index, out CanvasGroup selectedContainerCanvasGroup, out IReadOnlyList<CanvasGroup> otherContainersCanvasGroups)
        {
            selectedContainerCanvasGroup = containers[index].GetComponent<CanvasGroup>();
            var otherContainers = new List<Transform>(containers);
            otherContainers.RemoveAt(index);
            var list = new List<CanvasGroup>(otherContainers.Count);

            for (int i = 0; i < otherContainers.Count; i++)
            {
                list.Add(otherContainers[i].GetComponent<CanvasGroup>());
            }

            otherContainersCanvasGroups = list;
        }

        private void ResetProgressiveOperationsController()
        {
            _progressiveOperationsController.Clear();
        }

        private void Update()
        {
            _progressiveOperationsController.Update();
        }
    }
}