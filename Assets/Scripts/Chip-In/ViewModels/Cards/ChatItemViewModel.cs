using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;
using UnityWeld.Binding;
using Views.ViewElements.Interfaces;
using Views.ViewElements.ScrollViews.Adapters;

namespace ViewModels.Cards
{
    [Binding]
    public class ChatItemViewModel : MonoBehaviour, IFillingView<ChatMessageItemData>, INotifyPropertyChanged
    {
        private bool _triangleIsNoTheLeft;
        private string _text;
        private string _name;
        private Sprite _icon;

        [Binding]
        public string Text
        {
            get => _text;
            set
            {
                if (_text != value) return;
                _text = value;
                OnPropertyChanged();
            }
        }

        [Binding]
        public string Name
        {
            get => _name;
            set
            {
                if (_name == value) return;
                _name = value;
                OnPropertyChanged();
            }
        }

        [Binding]
        public Sprite Icon
        {
            get => _icon;
            set
            {
                _icon = value;
                OnPropertyChanged();
            }
        }

        [Binding]
        public bool TriangleIsNoTheLeft
        {
            get => _triangleIsNoTheLeft;
            set
            {
                if (_triangleIsNoTheLeft == value) return;
                _triangleIsNoTheLeft = value;
                OnPropertyChanged();
            }
        }

        public Task FillView(ChatMessageItemData data, uint dataBaseIndex)
        {
            Name = data.Name;
            Text = data.SurveyMessage;
            Icon = data.AvatarIcon;
            
            switch (data.TrianglePlacementSize)
            {
                case ChatMessageItemData.Side.Left:
                    TriangleIsNoTheLeft = true;
                    break;
                case ChatMessageItemData.Side.Right:
                    TriangleIsNoTheLeft = false;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return Task.CompletedTask;
        }


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}