using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Mechanics.Cell;
using ViewModel.Annotations;

namespace ViewModel
{
    public class Cell
    {
        private static readonly ReadOnlyCollection<NumericValue> _allNumericValues;

        static Cell()
        {
            _allNumericValues = new ReadOnlyCollection<NumericValue>(Enum.GetValues(typeof(NumericValue)).Cast<NumericValue>().ToList());
        }


        public ICommand Click
        {
            get;
            set;
        }

        public const int Width = 9;

        public Cell(ICell cell, Field parent, int x, int y)
        {
            var values = new List<int>();
            Result = "?";

            foreach (var value in _allNumericValues)
            {
                if (cell.MayBe(value))
                {
                    values.Add((int)value);
                }
            }

            Values = values.Select(value => Tuple.Create(value, new RelayCommand(() => parent.ValueChoosen(value, x, y))));


        }

        public IEnumerable<Tuple<int, RelayCommand>> Values
        {
            get;
            set;
        }

        public string Result { get; set; }

    }
}
