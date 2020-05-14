using System.Collections.Generic;
using DataModels.MatchModels;
using ScriptableObjects.Parameters;
using UnityEngine;
using ViewModels.UI.Elements.Icons;

namespace Controllers.SlotsSpinningControllers
{
    public class SlotSpinnerController : MonoBehaviour
    {
        private SlotSpinner _slotSpinner;
        [SerializeField] private SlotSpinnerProperties parameters;
        [SerializeField] private GameSlotIconView slotPrefab;

        private Dictionary<uint, uint> _correspondingIndexesDictionary;
        private GameSlotIconView[] _spinningElements;

        public uint ItemToFocusOnIndexFromIconId
        {
            get => _slotSpinner.ItemToFocusOnIndex;
            set => _slotSpinner.ItemToFocusOnIndex = _correspondingIndexesDictionary[value];
        }

        public uint ItemToFocusOnIndex
        {
            get => _slotSpinner.ItemToFocusOnIndex;
            set => _slotSpinner.ItemToFocusOnIndex = value;
        }

        private void OnEnable()
        {
            SetSlotSpinner();
        }

        private void SetSlotSpinner()
        {
            if (!TryGetComponent(out _slotSpinner))
                _slotSpinner = transform.GetChild(0).GetComponent<SlotSpinner>();
            _slotSpinner.SlotSpinnerProperties = parameters;
        }

        private void Start()
        {
            _slotSpinner.Stop();
        }

        public void AlignItems()
        {
            SetSlotSpinner();
            _slotSpinner.AlignItems();
        }

        public void SlideInstantlyToIndexPosition(uint index)
        {
            SetSlotSpinner();
            _slotSpinner.SlideInstantlyToIndexPosition(index);
        }

        public void StartElementsSpinning()
        {
            _slotSpinner.StartSpinning();
        }

        public void InitializeSpinningElements()
        {
            SetSlotSpinner();
            _slotSpinner.Initialize();
        }

        public void InitializeSpinningElements(int elementsNumber)
        {
            SetSlotSpinner();
            _spinningElements = _slotSpinner.Initialize(slotPrefab, elementsNumber);
        }

        public void PrepareItems(List<BoardIconData> animatedIconResource, float slotsSpritesAnimationSwitchingInterval,
            bool loopTheAnimation)
        {
            InitializeSpinningElements(animatedIconResource.Count);
            CreateCorrespondingIndexesDictionary(animatedIconResource);

            for (int i = 0; i < _spinningElements.Length; i++)
            {
                _spinningElements[i].InitializeAnimator(animatedIconResource[i].AnimatedIconResource,
                    slotsSpritesAnimationSwitchingInterval, loopTheAnimation);
            }
        }

        private void CreateCorrespondingIndexesDictionary(IReadOnlyList<BoardIconData> boardIconData)
        {
            _correspondingIndexesDictionary = new Dictionary<uint, uint>(boardIconData.Count);
            for (int i = 0; i < boardIconData.Count; i++)
            {
                _correspondingIndexesDictionary.Add((uint) boardIconData[i].Id, (uint) i);
            }
        }

        public void StartAnimating()
        {
            for (int i = 0; i < _spinningElements.Length; i++)
            {
                _spinningElements[i].StartAnimating();
            }
        }

        public void SetActivityState(bool active)
        {
            for (int i = 0; i < _spinningElements.Length; i++)
            {
                _spinningElements[i].ActivityState = active;
            }
        }
    }
}