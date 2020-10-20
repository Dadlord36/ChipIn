using DataModels.Interfaces;

namespace DataModels
{
    public class UserAnswer : IText, IQuestionId
    {
        public string Text { get; set; }
        public int QuestionId { get; set; }

        public UserAnswer(string text, int questionId)
        {
            Text = text;
            QuestionId = questionId;
        }
    }
}