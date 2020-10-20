using DataModels.Interfaces;

namespace DataModels
{
    public class SurveyQuestionDataModel : ISurveyQuestionModel
    {
        public string TextWithQuestion { get; set; }
        public string[] Answers { get; set; }
        public int? Id { get; set; }
    }
}