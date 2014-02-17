using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mechanics.Cell
{
    [Serializable]
    public class Cell : ICell
    {
        public override int GetHashCode()
        {
            return (_possibleValues.Count);
        }

        public object Clone()
        {
            var copyExcludeValue = new List<NumericValue>(_possibleValues);
            return new Cell(copyExcludeValue);
        }

        public override bool Equals(object obj)
        {
            var other = obj as Cell;
            return other != null && SameContent(this, other);
        }

        internal static readonly ReadOnlyCollection<NumericValue> _allNumericValues;

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

        public bool CouldBe(NumericValue value)
        {
            return _possibleValues.Contains(value);
        }

        public ICell ExcludeValue(NumericValue value)
        {

            if (IsDefined && _possibleValues.Contains(value))
            {
                throw new ArgumentException();
            }


            var copyExcludeValue = new List<NumericValue>(_possibleValues);
            copyExcludeValue.Remove(value);

            copyExcludeValue.Sort();

            return new Cell(copyExcludeValue);
        }

        public NumericValue Value
        {
            get
            {
                if (!IsDefined)
                {
                    throw new NotSupportedException();
                }

                return _possibleValues.First();
            }

        }

        private static bool SameContent(Cell a, Cell b)
        {

            if (a._possibleValues.Count != b._possibleValues.Count)
            {
                return false;
            }

            return !a._possibleValues.Where((t, i) => t != b._possibleValues[i]).Any();
        }
    }
}
