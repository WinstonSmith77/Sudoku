using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using System.Windows.Input;
using Mechanics;
using Mechanics.Cell;
using Mechanics.Exceptions;
using Mechanics.Geometry;
using Mechanics.GridManager;
using ViewModel.Annotations;

namespace ViewModel
{
    public sealed class GridManager : INotifyPropertyChanged
    {
        private IGridManager _gridManager;

        public GridManager()
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
            var dlg = new OpenFileDialog { DefaultExt = "sudoku", Filter = "Sodoku | *.sudoku", CheckFileExists = true };
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                using (var file = new FileStream(dlg.FileName, FileMode.Open))
                {
                    _gridManager = Factory.Instance.LoadGridManager(file);
                    CurrentGrid = new Grid(_gridManager.CurrentGrid, this);
                }
            }
        }

        private bool CanResetOrSave()
        {
            return _gridManager.CanReset();
        }

        private void SaveInner()
        {
            var dlg = new SaveFileDialog { DefaultExt = "sudoku", Filter = "Sodoku | *.sudoku" };

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                using (var file = new FileStream(dlg.FileName, FileMode.CreateNew))
                {
                    _gridManager.Save(file);
                }
            }
        }

        private bool CanRedo()
        {
            return _gridManager.CanRedo();
        }

        private void RedoInner()
        {
            CurrentGrid.SetGrid(_gridManager.Redo());
        }

        private void UndoInner()
        {
            CurrentGrid.SetGrid(_gridManager.Undo());
        }

        private bool CanUndo()
        {
            return _gridManager.CanUndo();
        }

        private void ResetInner()
        {
            _gridManager = Factory.Instance.CreateNewGridManager();
            CurrentGrid = new Grid(_gridManager.CurrentGrid, this);
        }

        private Grid _currentGrid;
        public Grid CurrentGrid
        {
            get { return _currentGrid; }
            set
            {
                if (Equals(value, _currentGrid)) return;
                _currentGrid = value;
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
                var newGrid = _gridManager.SetCell(p, (NumericValue)value);
                CurrentGrid.SetGrid(newGrid);
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
