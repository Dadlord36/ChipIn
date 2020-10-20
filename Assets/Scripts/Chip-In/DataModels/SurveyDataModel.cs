using DataModels.Interfaces;

namespace DataModels
{
    public class SurveyDataModel : ISurveyModel
    {
        public int? Id { get; set; }
        public string Title { get; set; }
        public SurveyQuestionDataModel[] Questions { get; set; }
    }
}