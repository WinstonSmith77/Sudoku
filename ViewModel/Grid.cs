using System.Collections.ObjectModel;
using System.ComponentModel;
using Mechanics.Geometry;
using Mechanics.Grid;
using ViewModel.Properties;

namespace ViewModel
{
    public sealed class Grid : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public Grid(IGrid grid, GridManager parent)
        {
            _parent = parent;
            _grid = grid;
            Cells = new ObservableCollection<Cell>(new Cell[Cell.Width * Cell.Width]);

            for (int x = 0; x < Cell.Width; x++)
            {
                for (int y = 0; y < Cell.Width; y++)
                {
                    var p = new Point(x, y);
                    Cells[p.Index] = new Cell(_grid[p], this, p);
                }
            }
        }

        public void SetGrid(IGrid grid)
        {
            _grid = grid;

            for (int x = 0; x < Cell.Width; x++)
            {
                for (int y = 0; y < Cell.Width; y++)
                {
                    var p = new Point(x, y);
                    if (!Cells[p.Index].InnerCell.Equals(_grid[p]))
                    {
                        Cells[p.Index] = new Cell(_grid[p], this, p);
                    }
                }
            }
        }

        private readonly GridManager _parent;
        private IGrid _grid;

        [UsedImplicitly]
        public ObservableCollection<Cell> Cells
        {
            get;
            private set;
        }

        internal void ValueChoosen(int value, Point p)
        {
            _parent.ValueChoosen(value, p);
        }
    }
}
