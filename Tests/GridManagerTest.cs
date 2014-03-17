using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mechanics;
using Mechanics.Cell;
using Mechanics.Geometry;
using Mechanics.GridManager;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class GridManagerTest
    {
        [Test]
        public void LoadAndSave()
        {
            byte[] array;
            IGridManager fmA;
            using (var stream = new MemoryStream())
            {
                fmA = Factory.Instance.CreateNewGridManager();
                fmA.Save(stream);
                stream.ToArray();
                array = stream.ToArray();
            }

            LoadAnCompare(array, fmA);
        }

        [Test]
        public void LoadAndSave2()
        {
            byte[] array;
            IGridManager fmA;
            using (var stream = new MemoryStream())
            {
                fmA = Factory.Instance.CreateNewGridManager();
                fmA.SetCell(new Point(3, 4), NumericValue.Seven);
                fmA.Save(stream);
                stream.ToArray();
                array = stream.ToArray();
            }

            LoadAnCompare(array, fmA);
        }

        private static void LoadAnCompare(byte[] array, IGridManager fmA)
        {
            using (var stream = new MemoryStream(array))
            {
                var fmB = Factory.Instance.LoadGridManager(stream);
                Assert.That(fmA.Equals(fmB));
            }
        }
    }
}
