using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using Mechanics;
using Mechanics.Cell;
using Mechanics.Exceptions;
using Mechanics.Geometry;
using ViewModel.Annotations;

namespace ViewModel
{
    public sealed class FieldManager : INotifyPropertyChanged
    {
        private Mechanics.FieldManager.IFieldManager _fieldManager;


        public FieldManager()
        {
            Reset = new RelayCommand(ResetInner, CanResetOrSave);
            ResetInner();

            Undo = new RelayCommand(UndoInner, CanUndo);
            Redo = new RelayCommand(RedoInner, CanRedo);
            Save = new RelayCommand(SaveInner, CanResetOrSave);

            Save = new RelayCommand(SaveInner, CanResetOrSave);
            Load = new RelayCommand(InnerLoad);
        }

        private void InnerLoad()
        {
            var dlg = new OpenFileDialog() { DefaultExt = "sudoku", Filter = "Sodoku | *.sudoku", CheckFileExists = true };
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                _fieldManager = Factory.Instance.LoadFieldManager(dlg.FileName);
                CurrentField = new Field(_fieldManager.CurrentField, this);
            }
        }

        private bool CanResetOrSave()
        {
            return _fieldManager.CanReset();
        }

        private void SaveInner()
        {
            var dlg = new SaveFileDialog { DefaultExt = "sudoku", Filter = "Sodoku | *.sudoku" };

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                _fieldManager.Save(dlg.FileName);
            }
        }

        private bool CanRedo()
        {
            return _fieldManager.CanRedo();
        }

        private void RedoInner()
        {
            CurrentField.SetField(_fieldManager.Redo());
        }

        private void UndoInner()
        {
            CurrentField.SetField(_fieldManager.Undo());
        }

        private bool CanUndo()
        {
            return _fieldManager.CanUndo();
        }

        private void ResetInner()
        {
            _fieldManager = Factory.Instance.CreateEmptyFieldManager();
            CurrentField = new Field(_fieldManager.CurrentField, this);
        }


        private Field _currentField;
        public Field CurrentField
        {
            get { return _currentField; }
            set
            {
                if (Equals(value, _currentField)) return;
                _currentField = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public void ValueChoosen(int value, Point p)
        {
            try
            {
                var newField = _fieldManager.SetCell(p, (NumericValue)value);
                CurrentField.SetField(newField);
            }
            catch (NoMoreSolutionException)
            {
                MessageBox.Show("Choice is not possible!", "Error");
            }

        }

        public ICommand Reset { get; private set; }
        public ICommand Undo { get; private set; }
        public ICommand Redo { get; private set; }

        public ICommand Save { get; private set; }
        public ICommand Load { get; private set; }
    }
}
