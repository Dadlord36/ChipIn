using System;
using UnityEngine;
using Views.ViewElements.ScrollViews.Adapters.BaseAdapters;

namespace DataModels
{
    public class ChatMessageItemData : ResizableItemData
    {
        public enum EMessageType
        {
            First,
            Other
        }

        public readonly string Name;
        public readonly EMessageType MessageType;
        public readonly string SurveyMessage;
        public readonly Sprite AvatarIcon;
        public readonly DateTime InitialTime;


        public ChatMessageItemData(EMessageType messageType, string surveyMessage, Sprite avatarIcon, string name, DateTime initialTime)
        {
            Name = name;
            InitialTime = initialTime;
            MessageType = messageType;
            SurveyMessage = surveyMessage;
            AvatarIcon = avatarIcon;
        }
    }
    
    public class UserChatMessageItemData : ChatMessageItemData
    {
        public readonly int QuestionId;

        public UserChatMessageItemData(EMessageType messageType, string surveyMessage, Sprite avatarIcon, string name, DateTime initialTime,
            int questionId) :
            base(messageType, surveyMessage, avatarIcon, name, initialTime)
        {
            QuestionId = questionId;
        }
    }

    public class ChatMessageItemDataExtendedData : UserChatMessageItemData
    {
        public readonly string[] PredefinedAnswers;

        public ChatMessageItemDataExtendedData(EMessageType messageType, string surveyMessage, Sprite avatarIcon, string name, DateTime initialTime,
            int questionId, string[] predefinedAnswers) : base(messageType, surveyMessage, avatarIcon, name, initialTime, questionId)
        {
            PredefinedAnswers = predefinedAnswers;
        }
    }
}