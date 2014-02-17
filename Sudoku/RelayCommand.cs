﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Sudoku
{

    public class RelayCommand : ICommand
    {
        private readonly Action _execute;
        private readonly Func<bool> _canExecute;

        public RelayCommand(Action execute)
            : this(execute, () => true)
        {
        }

        public RelayCommand(Action execute, Func<bool> canExecute)
        {
            if (execute == null)
            {
                throw new ArgumentNullException("execute");
            }

            _execute = execute;
            _canExecute = canExecute;
        }

        [DebuggerStepThrough]
        public bool CanExecute(object dummy)
        {
            return _canExecute();
        }

        public event EventHandler CanExecuteChangedInner;

        public event EventHandler CanExecuteChanged
        {
            add
            {
                CanExecuteChangedInner += value;
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CanExecuteChangedInner -= value;
                CommandManager.RequerySuggested -= value;
            }
        }

        public void FireCanExecuteChanged()
        {
            if (CanExecuteChangedInner != null)
            {
                CanExecuteChangedInner(this, EventArgs.Empty);
            }
        }

        public void Execute(object parameter)
        {
            _execute();
        }
    }

}