using System;
using System.Windows.Input;

namespace UI.ViewModel
{
    public abstract class WorkspaceViewModel : ViewModelBase
    {
        private RelayCommand _closeCommand;

        protected WorkspaceViewModel()
        {
        }

        /// <summary>
        /// Returns the command that, when invoked, attempts
        /// to remove this workspace from the user interface.
        /// </summary>
        public ICommand CloseCommand
        {
            get { return _closeCommand ?? (_closeCommand = new RelayCommand(param => this.OnRequestClose())); }
        }

        /// <summary>
        /// Raised when this workspace should be removed from the UI.
        /// </summary>
        public event EventHandler RequestClose;

        void OnRequestClose()
        {
            var handler = this.RequestClose;
            handler?.Invoke(this, EventArgs.Empty);
        }

    }
}
