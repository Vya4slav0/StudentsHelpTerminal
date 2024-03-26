using System;

namespace SettingsEditor.Infrastructure.Commands
{
    internal class RelayCommand : Base.CommandBase
    {
        private readonly Action<object> _Execute;
        private readonly Predicate<object> _CanExecute;

        public RelayCommand(Action<object> _Execute, Predicate<object> _CanExecute = null)
        {
            this._Execute = _Execute ?? throw new ArgumentNullException(nameof(_Execute));
            this._CanExecute = _CanExecute;
        }

        public override bool CanExecute(object parameter) => _CanExecute?.Invoke(parameter) ?? true;

        public override void Execute(object parameter) => _Execute(parameter);
    }
}
