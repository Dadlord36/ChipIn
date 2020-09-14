using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using JetBrains.Annotations;
using Repositories.Local;
using Tasking;
using UnityEngine;
using UnityWeld.Binding;

namespace TempFolder
{
    [Binding]
    public sealed class LoadImageTest : MonoBehaviour, INotifyPropertyChanged
    {
        [SerializeField] private string imagePath;
        [SerializeField] private DownloadedSpritesRepository downloadedSpritesRepository;

        [Binding]
        public Sprite Sprite
        {
            get => _sprite;
            set
            {
                _sprite = value;
                OnPropertyChanged();
            }
        }

        private Sprite _sprite;
        
        public async void Start()
        {
            try
            {
                var sprite = await downloadedSpritesRepository.CreateLoadSpriteTask(imagePath, CancellationToken.None, true);
               TasksFactories.ExecuteOnMainThread(() => { Sprite = sprite;});
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
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