using System;
using UnityEngine;

namespace Controllers
{
    [CreateAssetMenu(fileName = nameof(ViewsLogoController), menuName = nameof(Controllers) + "/" + nameof(ViewsLogoController), order = 0)]
    public sealed class ViewsLogoController : ScriptableObject
    {
        private Sprite _logoSpite;
        public event Action<Sprite> LogoChanged;

        [SerializeField] private Sprite defaultLogo;

        public Sprite LogoSpite
        {
            get
            {
                if (!_logoSpite)
                {
                    _logoSpite = defaultLogo;
                }

                return _logoSpite;
            }
            set
            {
                _logoSpite = value;
                OnLogoChanged(_logoSpite);
            }
        }

        public void SetDefaultLogo()
        {
            LogoSpite = defaultLogo;
        }

        private void OnLogoChanged(Sprite obj)
        {
            LogoChanged?.Invoke(obj);
        }
    }
}