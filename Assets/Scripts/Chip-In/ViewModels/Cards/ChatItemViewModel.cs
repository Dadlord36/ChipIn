using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using DataModels;
using JetBrains.Annotations;
using Tasking;
using UnityEngine;
using UnityWeld.Binding;
using Utilities;
using Views.ViewElements.Interfaces;

namespace ViewModels.Cards
{
    [Binding]
    public class ChatItemViewModel : MonoBehaviour, IFillingView<ChatMessageItemData>, INotifyPropertyChanged
    {
        [SerializeField] private GameObject[] firstMessageElements;
        [SerializeField] private GameObject[] otherMessageElements;

        private string _text;
        private string _name;
        private Sprite _icon;
        private DateTime _initialTime;


        [Binding]
        public string Text
        {
            get => _text;
            set
            {
                if (_text == value) return;
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
        public DateTime InitialTime
        {
            get => _initialTime;
            set
            {
                _initialTime = value;
                OnPropertyChanged();
            }
        }

        private bool _iconShouldBeSeen;

        [Binding]
        public bool IconShouldBeSeen
        {
            get => _iconShouldBeSeen;
            set
            {
                if (_iconShouldBeSeen == value) return;
                _iconShouldBeSeen = value;
                OnPropertyChanged();
            }
        }

        public Task FillView(ChatMessageItemData data, uint dataBaseIndex)
        {
            Name = data.Name;
            Text = data.SurveyMessage;
            Icon = data.AvatarIcon;
            InitialTime = data.InitialTime;

            switch (data.MessageType)
            {
                case ChatMessageItemData.EMessageType.First:
                    SwitchToFirstMessageAppearance();
                    IconShouldBeSeen = true;
                    break;
                case ChatMessageItemData.EMessageType.Other:
                    SwitchToOtherMessageAppearance();
                    IconShouldBeSeen = false;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return Task.CompletedTask;
        }

        private void SwitchToFirstMessageAppearance()
        {
            GameObjectsUtility.SetGameObjectsActivity(firstMessageElements, true);
            GameObjectsUtility.SetGameObjectsActivity(otherMessageElements, false);
        }

        private void SwitchToOtherMessageAppearance()
        {
            GameObjectsUtility.SetGameObjectsActivity(otherMessageElements, true);
            GameObjectsUtility.SetGameObjectsActivity(firstMessageElements, false);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            TasksFactories.ExecuteOnMainThread(() => { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); });
        }
    }
}