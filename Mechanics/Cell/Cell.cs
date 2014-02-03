using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mechanics.Cell
{
    public class Cell : ICell
    {
        private static readonly ReadOnlyCollection<NumericValue> _allNumericValues;

        static Cell()
        {
            _allNumericValues = new ReadOnlyCollection<NumericValue>(Enum.GetValues(typeof(NumericValue)).Cast<NumericValue>().ToList());
        }

        public static Cell CreateEmptyCell()
        {
            return new Cell(_allNumericValues);
        }

        private Cell(IEnumerable<NumericValue> possibleValues)
        {
            _possibleValues = new List<NumericValue>(possibleValues);
        }

        private readonly List<NumericValue> _possibleValues;



        public bool IsDefined
        {
            get
            {
                return _possibleValues.Count == 1;
            }

        }

        public bool MayBe(NumericValue value)
        {
            return _possibleValues.Contains(value);
        }

        public ICell ExcludeValue(NumericValue value)
        {
            if (IsDefined)
            {
                throw new ArgumentException("Cell is already is defined!");
            }

            var copyExcludeValue = new List<NumericValue>(_possibleValues);
            if (!copyExcludeValue.Remove(value))
            {
                throw new ArgumentException(value.ToString() + " already not possible anymore!");
            }

            return new Cell(copyExcludeValue);
        }
    }
}
