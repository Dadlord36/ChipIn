using ViewModels;

namespace Commands
{
    public class UpdatePasswordCommand : BaseCommand<LoginViewModel>
    {

        public override bool CanExecute(object parameter)
        {
            return viewModel.CanReceiveInput;
        }

        public override void Execute(object parameter)
        {
            viewModel.SetUserPassword(parameter.ToString());
        }
    }
}