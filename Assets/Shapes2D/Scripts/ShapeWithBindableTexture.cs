using System.ComponentModel;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;
using UnityWeld.Binding;

namespace Shapes2D
{
    [Binding]
    public sealed class ShapeWithBindableTexture : Shape, INotifyPropertyChanged
    {
        [Binding]
        public Texture2D FillTexture
        {
            get => settings.fillTexture;
            set
            {
                settings.fillTexture = value;
                OnPropertyChanged();
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}