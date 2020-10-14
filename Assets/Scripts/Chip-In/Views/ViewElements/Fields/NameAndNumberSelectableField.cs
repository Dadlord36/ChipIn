using System.Threading.Tasks;
using DataModels;
using UnityWeld.Binding;

namespace Views.ViewElements.Fields
{
    [Binding]
    public sealed class NameAndNumberSelectableField : NameAndNumberSelectableFieldBase<AnswerData>
    {
        public NameAndNumberSelectableField() : base(nameof(NameAndNumberSelectableField))
        {
        }
        
        public override Task FillView(AnswerData data, uint dataBaseIndex)
        {
            base.FillView(data, dataBaseIndex);
            Name = data.Answer;
            Number = data.Percent.ToString();
            return Task.CompletedTask;
        }
    }
}