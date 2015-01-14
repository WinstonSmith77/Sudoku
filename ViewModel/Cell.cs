using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows.Media;
using Mechanics.Cell;
using Mechanics.Geometry;
using ViewModel.Properties;

namespace ViewModel
{
    public class Cell
    {
        private static readonly ReadOnlyCollection<NumericValue> _allNumericValues;

        static Cell()
        {
            _allNumericValues = new ReadOnlyCollection<NumericValue>(Enum.GetValues(typeof(NumericValue)).Cast<NumericValue>().ToList());
        }

        public const int Width = 9;

        private readonly ICell _cell;

        [UsedImplicitly]
        public bool IsDefined
        {
            get
            {
                return _cell.IsDefined;
            }
        }

        public Cell(ICell cell, Grid parent, Point p)
        {
            _cell = cell;
            var values = new List<int>();

            if (IsDefined)
            {
                var value = (int) (cell.Value);
                Result = value.ToString(CultureInfo.InvariantCulture);
                ResultColor = _cellColors[value - 1];
            }
            else
            {
                foreach (var value in _allNumericValues)
                {
                    if (cell.MayHaveValue(value))
                    {
                        values.Add((int) value);
                    }
                }

                Values =
                    Enumerable.Range(1, Width)
                              .ToList()
                              .Select(
                                  value =>
                                  Tuple.Create(value, new RelayCommand(() => parent.ValueChoosen(value, p)),
                                               values.Contains(value), _cellColors[value - 1]));
                Result = "?";
            }
           
        }

        [UsedImplicitly]
        public IEnumerable<Tuple<int, RelayCommand, bool, Color>> Values
        {
            get;
            set;
        }

        [UsedImplicitly]
        public string Result { get; private set; }

        [UsedImplicitly]
        public Color ResultColor { get; private set; }

        public ICell InnerCell
        {
            get
            {
                return _cell;
            }
        }

        private static readonly Color[] _cellColors = { 
            Colors.Red, Colors.Green, Colors.Peru, 
            Colors.Blue, Colors.Black, Colors.DarkRed, 
            Colors.Orange, Colors.Purple, Colors.DarkGoldenrod };
    }
}
