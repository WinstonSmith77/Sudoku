using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mechanics;
using Mechanics.Cell;
using NUnit.Framework;

namespace Tests.CellTests
{
    [TestFixture]
    public class Cell
    {
        static Cell()
        {
            _allNumericValues = new ReadOnlyCollection<NumericValue>(Enum.GetValues(typeof(NumericValue)).Cast<NumericValue>().ToList());
        }

        private static readonly ReadOnlyCollection<NumericValue> _allNumericValues;

        [Test]
        public void ConstructorTestEmptyCell()
        {
            var cell = Factory.Instance.CreateEmptyCell();

            Assume.That(!cell.IsDefined);

            foreach (var numericValue in _allNumericValues)
            {
                Assume.That(cell.MayBe(numericValue));
            }

        }

        [Test]
        public void ExcludeValues()
        {
            var cell = Factory.Instance.CreateEmptyCell();

            foreach (var numericValue in _allNumericValues)
            {
                var cellCopy = cell.ExcludeValue(numericValue);
                Assume.That(!cellCopy.MayBe(numericValue));
            }
        }

        [Test]
        public void ExcludeValues2()
        {
            var cell = Factory.Instance.CreateEmptyCell();

            for (int indexToExclude = 0; indexToExclude < _allNumericValues.Count - 1; indexToExclude++)
            {
                cell = cell.ExcludeValue(_allNumericValues[indexToExclude]);
                for (int indexToTest = 0; indexToTest <= indexToExclude; indexToTest++)
                {
                    Assume.That(!cell.MayBe(_allNumericValues[indexToTest]));
                }

                for (int indexToTest = indexToExclude + 1; indexToTest < _allNumericValues.Count; indexToTest++)
                {
                    Assume.That(cell.MayBe(_allNumericValues[indexToTest]));
                }
            }
        }

        [Test]
        public void ExcludeInvokeException()
        {
            var cell = Factory.Instance.CreateEmptyCell();

            cell = cell.ExcludeValue(NumericValue.Eight);

            Assert.Throws<ArgumentException>(() => cell.ExcludeValue(NumericValue.Eight));
        }

        [Test]
        public void ExcludeInvokeException2()
        {
            var cell = Factory.Instance.CreateEmptyCell();

            foreach (var numericValue in _allNumericValues)
            {
                if (numericValue == NumericValue.Nine)
                {
                    Assert.Throws<ArgumentException>(() => cell.ExcludeValue(numericValue));
                }
                else
                {
                    cell = cell.ExcludeValue(numericValue);
                }
            }
        }

        [Test]
        public void CheckForIsDefined()
        {
            var cell = Factory.Instance.CreateEmptyCell();

            foreach (var numericValue in _allNumericValues)
            {
                Assume.That(!cell.IsDefined);

                if (numericValue == NumericValue.Five)
                {
                    continue;
                }

                cell = cell.ExcludeValue(numericValue);
            }

            Assume.That(cell.IsDefined);
        }

        [Test]
        public void IsEqual()
        {
            var range = new List<NumericValue>(_allNumericValues);
            range.Remove(NumericValue.Eight);

            var shuffledA = range.Shuffle(25);
            var shuffledB = range.Shuffle(890);

            var cellA = Factory.Instance.CreateEmptyCell();
            var cellB = Factory.Instance.CreateEmptyCell();

            Assume.That(cellA.Equals(cellB));

            foreach (var remove in shuffledA)
            {
                cellA = cellA.ExcludeValue(remove);
            }

           
            foreach (var remove in shuffledB)
            {
                cellB = cellB.ExcludeValue(remove);
            }

            Assume.That(cellA.Equals(cellB));
        }

        [Test]
        public void IsNotEqual()
        {
            var range = new List<NumericValue>(_allNumericValues);
            range.Remove(NumericValue.Eight);

            var shuffledA = range.Shuffle(25);
            var shuffledB = range.Shuffle(890);
            shuffledB.RemoveAt(0);

            var cellA = Factory.Instance.CreateEmptyCell();
            var cellB = Factory.Instance.CreateEmptyCell();

            Assert.That(!cellA.Equals(null));
           

            foreach (var remove in shuffledA)
            {
                cellA = cellA.ExcludeValue(remove);
            }


            foreach (var remove in shuffledB)
            {
                cellB = cellB.ExcludeValue(remove);
            }

            Assume.That(!cellA.Equals(cellB));
        }

        //public 
    }
}
