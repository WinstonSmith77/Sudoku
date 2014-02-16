using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mechanics.Cell
{
    public interface ICell : ICloneable
    {
        bool IsDefined
        {
            get;
        }

        bool MayBe(NumericValue value);

        ICell ExcludeValue(NumericValue value);
    }
}
