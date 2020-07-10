using System;
using Common;
using Common.Interfaces;
using CustomAnimators;
using CustomAnimators.GeneratedAnimationActions;
using ScriptableObjects.SwitchBindings;
using UnityEngine;
using UnityEngine.EventSystems;
using Utilities;
using Views.ViewElements;

namespace ViewModels.UI
{
    public class ViewsSwitchingAnimationController : UIBehaviour
    {
        [SerializeField] private ViewsSwitchingAnimationBinding viewsSwitchingAnimationBinding;
        [SerializeField] private AnimationCurve speedCurve;
        [SerializeField] private AnimationCurve fadingCurve;
        [SerializeField, Space(10)] private float transitionTime = 1f;
        [SerializeField, Space(10)] private float fadingTime = 1f;

        [SerializeField, Space(10)] private ViewSlot previousViewSlot;
        [SerializeField, Space(10)] private ViewSlot nextViewSlot;
        
        private readonly ProgressiveOperationsController _mainProgressiveOperationsController = new ProgressiveOperationsController();
        private readonly ProgressiveOperationsController _secondaryProgressiveOperationsController = new ProgressiveOperationsController();

        private float _movementDistance;
        private Vector2 _centerDestinationPoint;

        private readonly OperationsCompletionTracker _progressiveOperationsCompletionTracker = new OperationsCompletionTracker();


        private void StartAnimation(ViewsSwitchingParameters viewsSwitchingParameters)
        {
            ClearProgressiveOperationsController();
            ResetViewSlotsAnimationParameters();
            _progressiveOperationsCompletionTracker.ResetCounter();

            if (viewsSwitchingParameters.PreviousViewAppearanceParameters != null)
                SetupPreviousViewSlotAnimation(viewsSwitchingParameters.PreviousViewAppearanceParameters);

            SetupNextViewAnimation(viewsSwitchingParameters.NextViewAppearanceParameters);
            StartUpdating();
        }

        private void ResetViewSlotsAnimationParameters()
        {
            previousViewSlot.CanvasGroup.alpha = 1;
            nextViewSlot.CanvasGroup.alpha = 1;
        }

        private void SetupViewSlotAnimation(ViewSlot viewSlot, ProgressiveOperationsController progressiveOperationsController,
            in ViewAppearanceParameters viewAppearanceParameters)
        {
            void StartProgressiveOperationInMainController(IUpdatableProgress progressiveOperation)
            {
                StartProgressiveOperation(progressiveOperationsController, progressiveOperation);
            }

            viewSlot.CanvasSortingOrder = EnumUtilities.GetSortingOrderFromSwitchingViewPosition(viewAppearanceParameters.SortingPosition);

            if (viewAppearanceParameters.ShouldFade)
            {
                var canvasGroup = viewSlot.CanvasGroup;
                canvasGroup.alpha = 0;
                StartProgressiveOperationInMainController(CreateFadingAnimation(canvasGroup));
            }

            if (viewAppearanceParameters.AppearanceType != ViewAppearanceParameters.Appearance.Stays)
            {
                StartProgressiveOperationInMainController(DeterminateAnimationForAppearance(viewSlot.transform, viewAppearanceParameters));
            }
            else
            {
                viewSlot.transform.position = _centerDestinationPoint;
            }
        }

        private void StartProgressiveOperation(ProgressiveOperationsController progressiveOperationsController, IUpdatableProgress progressiveOperation)
        {
            progressiveOperationsController.AddProgressiveOperation(progressiveOperation);
            progressiveOperationsController.ProgressReachesEnd += _progressiveOperationsCompletionTracker.ConfirmActionCompletion;
            _progressiveOperationsCompletionTracker.AddToCounter();
        }

        private void SetupPreviousViewSlotAnimation(ViewAppearanceParameters? previousViewAppearanceParameters)
        {
            SetupViewSlotAnimation(previousViewSlot, _mainProgressiveOperationsController,
                (ViewAppearanceParameters) previousViewAppearanceParameters);
        }

        private void SetupNextViewAnimation(in ViewAppearanceParameters nextViewAppearanceParameters)
        {
            SetupViewSlotAnimation(nextViewSlot, _secondaryProgressiveOperationsController, nextViewAppearanceParameters);
        }


        private IUpdatableProgress CreateFadingAnimation(CanvasGroup canvasGroup)
        {
            return new CanvasGroupFading(fadingCurve, fadingTime, canvasGroup);
        }

        private IUpdatableProgress DeterminateAnimationForAppearance(Transform objectTransform, in ViewAppearanceParameters appearanceParameters)
        {
            var angle = CircleUtility.GetDegreesAngleFromMovementDirection(appearanceParameters.Direction);

            switch (appearanceParameters.AppearanceType)
            {
                case ViewAppearanceParameters.Appearance.MoveOut:
                    return new MoveToPoint(objectTransform, _centerDestinationPoint,
                        CircleUtility.FindAnglePosition(_centerDestinationPoint, appearanceParameters.MaxPathPercentage *
                                                                                 _movementDistance, angle),
                        speedCurve, transitionTime);

                case ViewAppearanceParameters.Appearance.MoveIn:
                    return new MoveToPoint(objectTransform,
                        CircleUtility.FindAnglePosition(_centerDestinationPoint, appearanceParameters.MaxPathPercentage *
                                                                                 _movementDistance, angle), _centerDestinationPoint,
                        speedCurve, transitionTime);

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        protected override void Awake()
        {
            base.Awake();
            StopUpdating();
            SetMovementDistanceAndDestinations();
            viewsSwitchingAnimationBinding.ViewsSwitchingAnimationRequested += StartAnimation;
            _progressiveOperationsCompletionTracker.WhenAllIsDone += ProgressiveOperationsCompletionTrackerOnWhenAllIsDone;
        }

        private void ProgressiveOperationsCompletionTrackerOnWhenAllIsDone()
        {
            StopUpdating();
        }

        private void ClearProgressiveOperationsController()
        {
            _mainProgressiveOperationsController.Clear();
            _secondaryProgressiveOperationsController.Clear();
        }

        private void SetMovementDistanceAndDestinations()
        {
            _centerDestinationPoint = Vector2.zero;
            _movementDistance = CalculateScreenWidth();
        }

        private static float CalculateScreenWidth()
        {
            return ScreenUtility.GetScreenSize().x;
        }

        private void StartUpdating()
        {
            enabled = true;
        }

        private void StopUpdating()
        {
            enabled = false;
        }

        private void Update()
        {
            _mainProgressiveOperationsController.Update();
            _secondaryProgressiveOperationsController.Update();
        }
    }
}