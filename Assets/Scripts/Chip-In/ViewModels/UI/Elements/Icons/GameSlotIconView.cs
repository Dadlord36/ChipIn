using CustomAnimators;
using UnityEngine;
using UnityEngine.Assertions;

namespace ViewModels.UI.Elements.Icons
{
    [RequireComponent(typeof(SimpleImageAnimator))]
    public class GameSlotIconView : BaseIconView
    {
        #region Fields

        private bool _activityState;
        private static readonly Color InactiveColor = Color.gray;
        private static readonly Color ActiveColor = Color.white;
        private SimpleImageAnimator _simpleImageAnimator;

        #endregion


        #region Public Properties

        /*public Sprite SlotIcon
        {
            get => IconSprite;
            set => IconSprite = value;
        }*/

        public bool ActivityState
        {
            get => _activityState;
            set
            {
                _activityState = value;
                UpdateActivityStateRepresentation();
            }
        }

        #endregion

        #region Unity Event Functions

        protected override void OnEnable()
        {
            base.OnEnable();
            CollectComponentReferences();
            InitializeComponents();
        }

        #endregion


        #region Private Functions

        private void InitializeComponents()
        {
            _simpleImageAnimator.Initialize();
        }
        
        private void CollectComponentReferences()
        {
            Assert.IsTrue(TryGetComponent(out _simpleImageAnimator),
                $"There is not {nameof(SimpleImageAnimator)} on {name}");
        }

        private void UpdateActivityStateRepresentation()
        {
            if (ActivityState)
                MakeActive();
            else
            {
                MakeInactive();
            }
        }

        private void MakeInactive()
        {
            SetIconColor(InactiveColor);
        }

        private void MakeActive()
        {
            SetIconColor(ActiveColor);
        }

        #endregion

        #region IImageAnimator implementation delegation

        public void InitializeAnimator(SimpleImageAnimator.SpritesAnimatorResource spritesAnimatorResource,
            float updateInterval, bool loopTheAnimation)
        {
            _simpleImageAnimator.Setup(spritesAnimatorResource, updateInterval, loopTheAnimation);
        }

        public void StartAnimating()
        {
            _simpleImageAnimator.StartAnimating();
        }

        public void Pause()
        {
            _simpleImageAnimator.Pause();
        }

        public void StopAnimating()
        {
            _simpleImageAnimator.StopAnimating();
        }

        #endregion
    }
}