using System;
using UnityEngine;

namespace Controllers
{
    public class SlotsLauncher : MonoBehaviour
    {
        [SerializeField] private Transform[] targets;

        public void StartSpinning()
        {
            foreach (var target in targets)
            {
                ActivateIt(target);
            }
        }

        private void Start()
        {
            StartSpinning();
        }

        private void ActivateIt(Transform target)
        {
            target.GetComponent<SlotSpinner>().Initialize();
            

            foreach (Transform child in target)
            {
                var item = child.GetComponent<SlotSpinner>();
                if (item == null) continue;

                item.Initialize();
                item.StartSpinning();
            }
            target.GetComponent<SlotSpinner>().StartSpinning();
        }
    }
}