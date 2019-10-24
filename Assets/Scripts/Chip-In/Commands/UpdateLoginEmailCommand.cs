using ViewModels;

namespace Commands
{
    public class UpdateLoginEmailCommand : BaseCommand<LoginViewModel>
    {
        public override bool CanExecute(object parameter)
        {
            return viewModel.CanReceiveInput;
        }

        public override void Execute(object parameter)
        {
            viewModel.SetUserEmail(parameter.ToString());
        }
    }
}