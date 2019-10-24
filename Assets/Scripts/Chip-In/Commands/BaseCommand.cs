using System;
using System.Windows.Input;
using UnityEngine;
using ViewModels;

namespace Commands
{
    /// <summary>
    /// Base class for any ViewModel command
    /// </summary>
    /// <typeparam name="TViewModel">ViewModel type</typeparam>
    public abstract class BaseCommand<TViewModel> : MonoBehaviour, ICommand where TViewModel : BaseViewModel
    {
        public event EventHandler CanExecuteChanged;
        [SerializeField] protected TViewModel viewModel;

        protected void InvokeCanExecute()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
        public abstract bool CanExecute(object parameter);
        public abstract void Execute(object parameter);
    }
}