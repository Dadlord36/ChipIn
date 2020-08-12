using UnityEngine;

namespace Controllers
{
    public class RotationController : MonoBehaviour
    {
        [SerializeField] private float rotationSpeed;
        private Transform _thisTransform;

        private void Start()
        {
            _thisTransform = transform;
        }

        private Vector3 _tempRotationEuler;

        private void Update()
        {
            _tempRotationEuler.z += Time.deltaTime * rotationSpeed;
            _thisTransform.rotation = Quaternion.Euler(_tempRotationEuler);
        }
    }
}