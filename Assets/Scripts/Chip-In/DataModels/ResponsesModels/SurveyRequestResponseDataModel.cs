using DataModels.Interfaces;

namespace DataModels.ResponsesModels
{
    public class SurveyRequestResponseDataModel : ISurveyRequestResponseModel
    {
        public bool Success { get; set; }
        public SurveyDataModel Survey { get; set; }
    }
}