using TMPro;
using UnityEngine;

namespace Views.Cards
{
    public interface IAdminProperties
    {
        uint TokenBallance { get; set; }
        uint AdSpendField { get; set; }
        uint SalesFromThisApp { get; set; }
        uint SalesCommisions { get; set; }
        uint ReturnOnInvestments { get; set; }
    }

    public class AdminView : BaseView, IAdminProperties
    {
        [SerializeField] private TMP_Text tokenBallanceTextField;

        [SerializeField] private TMP_Text adSpendField;
        [SerializeField] private TMP_Text salesFromThisAppTextField;
        [SerializeField] private TMP_Text salesCommisionsTextField;
        [SerializeField] private TMP_Text returnOnInvestmentsTextField;

        public uint TokenBallance
        {
            get => uint.Parse(tokenBallanceTextField.text);
            set => tokenBallanceTextField.text = value.ToString();
        }

        public uint AdSpendField
        {
            get => uint.Parse(adSpendField.text);
            set => adSpendField.text = value.ToString();
        }

        public uint SalesFromThisApp
        {
            get => uint.Parse(salesFromThisAppTextField.text);
            set => salesFromThisAppTextField.text = value.ToString();
        }

        public uint SalesCommisions
        {
            get => uint.Parse(salesCommisionsTextField.text);
            set => salesCommisionsTextField.text = value.ToString();
        }

        public uint ReturnOnInvestments
        {
            get => uint.Parse(returnOnInvestmentsTextField.text);
            set => returnOnInvestmentsTextField.text = value.ToString();
        }

        public AdminView() : base(nameof(AdminView))
        {
        }
    }
}