using System;
using DataModels.Interfaces;

namespace DataModels
{
    public class VerificationDataModel : IVerificationModel
    {
        public int? Id { get; set; }
        public string Category { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
    }
}