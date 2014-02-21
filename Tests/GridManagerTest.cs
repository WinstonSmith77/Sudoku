using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mechanics;
using Mechanics.Cell;
using Mechanics.Geometry;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class GridManagerTest
    {
        [Test]
        public void LoadAndSave()
        {
            string tempFile = Path.GetTempFileName();
            try
            {
                var fmA = Factory.Instance.CreateNewGridManager();
                fmA.Save(tempFile);

                var fmB = Factory.Instance.LoadGridManager(tempFile);

                Assert.That(fmA.Equals(fmB));

                var fmC = Factory.Instance.CreateNewGridManager();
                fmC.SetCell(new Point(3, 4), NumericValue.Seven);
                fmC.Save(tempFile);

                var fmD = Factory.Instance.LoadGridManager(tempFile);

                Assert.That(fmC.Equals(fmD));
            }
            finally
            {
                File.Delete(tempFile);
            }
        }
    }
}
