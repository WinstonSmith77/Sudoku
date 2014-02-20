using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Mechanics;
using Mechanics.Cell;
using Mechanics.Exceptions;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class CellTests
    {
        static CellTests()
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
                Assume.That(cell.CouldBe(numericValue));
            }

        }

        [Test]
        public void ExcludeValues()
        {
            var cell = Factory.Instance.CreateEmptyCell();

            foreach (var numericValue in _allNumericValues)
            {
                var cellCopy = cell.ExcludeValue(numericValue);
                Assume.That(!cellCopy.CouldBe(numericValue));
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
                    Assume.That(!cell.CouldBe(_allNumericValues[indexToTest]));
                }

                for (int indexToTest = indexToExclude + 1; indexToTest < _allNumericValues.Count; indexToTest++)
                {
                    Assume.That(cell.CouldBe(_allNumericValues[indexToTest]));
                }
            }
        }

        [Test]
        public void ExcludeInvokeException()
        {
            var cell = Factory.Instance.CreateEmptyCell();
            Assert.Throws<ValueIsNotDefinedException>(() =>
                {
                    var a = cell.Value;
                });
        }

        [Test]
        public void CheckForIsDefinedAndValue()
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
            Assume.That(cell.Value == NumericValue.Five);
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

        [Test]
        public void Clone()
        {
            var cellA = Factory.Instance.CreateEmptyCell();
            var cellB = (ICell)cellA.Clone();

            Assume.That(cellA.Equals(cellB));

            var cellC = cellB.ExcludeValue(NumericValue.Eight);
            Assume.That(!cellA.Equals(cellC));

            Assume.That(cellA.Equals(cellB));
        }
    }
}
