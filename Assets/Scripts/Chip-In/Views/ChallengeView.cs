using UnityEngine;
using UnityEngine.UI;
using Views.ViewElements;

namespace Views
{
    public class ChallengeView : ContainerView<ChallengeCardView>
    {
        [SerializeField] private Button playButton;

        public bool PlayButtonInteractivity
        {
            get => playButton.interactable;
            set => playButton.interactable = value;
        }
    }
}