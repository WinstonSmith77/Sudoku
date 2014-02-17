using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mechanics;
using Mechanics.Cell;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class FieldManagerTest
    {

        [Test]
        public void LoadAndSave()
        {
            string tempFile = Path.GetTempFileName();
            try
            {
                var fmA = Factory.Instance.CreateEmptyFieldManager();
                fmA.Save(tempFile);

                var fmB = Factory.Instance.LoadFieldManager(tempFile);

                Assert.That(fmA.Equals(fmB));

                var fmC = Factory.Instance.CreateEmptyFieldManager();
                fmC.SetCell(new Point(3, 4), NumericValue.Seven);
                fmC.Save(tempFile);

                var fmD = Factory.Instance.LoadFieldManager(tempFile);

                Assert.That(fmC.Equals(fmD));
            }
            finally
            {
                File.Delete(tempFile);
            }
        }
    }
}
