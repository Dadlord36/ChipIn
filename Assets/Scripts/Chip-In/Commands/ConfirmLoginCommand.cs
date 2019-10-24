using ViewModels;

namespace Commands
{
    public class ConfirmLoginCommand : BaseCommand<LoginViewModel>
    {
        public override bool CanExecute(object parameter)
        {
            return viewModel.CanLogin;
        }

        public override void Execute(object parameter)
        {
            viewModel.LoginToAccount();
        }

        private void OnEnable()
        {
            viewModel.OnCanLoginStateChanged += ReInvokeCanExecute;
        }

        private void OnDisable()
        {
            viewModel.OnCanLoginStateChanged -= ReInvokeCanExecute;
        }

        void ReInvokeCanExecute(bool b)
        {
            InvokeCanExecute();
        }
    }
}