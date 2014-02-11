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
        public override int GetHashCode()
        {
            return (_possibleValues.Count);
        }

        public override bool Equals(object obj)
        {
            var other = obj as Cell;
            return other != null && SameContent(this, other);
        }

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
            var copyExcludeValue = new List<NumericValue>(_possibleValues);

            copyExcludeValue.Sort();

            return new Cell(copyExcludeValue);
        }

        public static bool SameContent(Cell a, Cell b)
        {

            if (a._possibleValues.Count != b._possibleValues.Count)
            {
                return false;
            }

            return !a._possibleValues.Where((t, i) => t != b._possibleValues[i]).Any();
        }
    }
}
