using UnityEngine;

namespace ScriptableObjects.Parameters
{
    [CreateAssetMenu(fileName = nameof(SwipeDetectorParameters), menuName = nameof(Parameters) + "/" + nameof(SwipeDetectorParameters), order = 0)]
    public class SwipeDetectorParameters : ScriptableObject
    {
        [SerializeField] private bool detectSwipeOnlyAfterRelease;
        [SerializeField] private float minDistanceForSwipe = 20f;

        public bool DetectSwipeOnlyAfterRelease => detectSwipeOnlyAfterRelease;
        public float MinDistanceForSwipe => minDistanceForSwipe;
    }
}