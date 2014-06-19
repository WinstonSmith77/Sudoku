using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using Mechanics.Cell;
using Mechanics.Geometry;
using ViewModel.Annotations;
using Mechanics;

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
                Result = value.ToString();
                ResultColor = MyColors[value - 1];
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
                                               values.Contains(value), MyColors[value - 1]));
                Result = "?";
            }
           
        }

        public IEnumerable<Tuple<int, RelayCommand, bool, Color>> Values
        {
            get;
            set;
        }

        public string Result { get; set; }
        public Color ResultColor { get; set; }

        public ICell InnerCell
        {
            get
            {
                return _cell;
            }
        }

        private static Color[] MyColors = new[] { 
            Colors.Red, Colors.Green, Colors.Peru, 
            Colors.Blue, Colors.Black, Colors.DarkRed, 
            Colors.Orange, Colors.Purple, Colors.DarkGoldenrod };
    }
}
