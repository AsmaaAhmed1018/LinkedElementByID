using System;
using System.Windows.Input;

namespace LinkedElementByID
{
    public class RelayCommands : ICommand
    {
        #region Event
        public event EventHandler CanExecuteChanged;

        #endregion
        #region Property
        public Action<object> Excute { get; set; }
        public Predicate<object> CanExcute { get; set; }
        #endregion
        #region Constructor
        public RelayCommands(Action<object> excute, Predicate<object> canExcute)
        {
            this.Excute = excute;
            this.CanExcute = canExcute;
        }
        #endregion
        #region Methods
        public bool CanExecute(object parameter)
        {
            return CanExcute(parameter);
        }
        public void Execute(object parameter)
        {
            Excute(parameter);
        }
        #endregion
    }
}
