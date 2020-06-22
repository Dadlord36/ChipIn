using TMPro;
using UnityEngine;

namespace Views.Cards
{
    public interface IAdminProperties
    {
        uint TokenBalance { get; set; }
        uint AdSpendField { get; set; }
        uint SalesFromThisApp { get; set; }
        uint SalesCommissions { get; set; }
        uint ReturnOnInvestments { get; set; }
    }

    public class AdminView : BaseView, IAdminProperties
    {
        [SerializeField] private TMP_Text tokenBalanceTextField;

        [SerializeField] private TMP_Text adSpendField;
        [SerializeField] private TMP_Text salesFromThisAppTextField;
        [SerializeField] private TMP_Text salesCommissionsTextField;
        [SerializeField] private TMP_Text returnOnInvestmentsTextField;

        public uint TokenBalance
        {
            get => uint.Parse(tokenBalanceTextField.text);
            set => tokenBalanceTextField.text = value.ToString();
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

        public uint SalesCommissions
        {
            get => uint.Parse(salesCommissionsTextField.text);
            set => salesCommissionsTextField.text = value.ToString();
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