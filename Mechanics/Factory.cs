using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mechanics.Cell;

namespace Mechanics
{
    public class Factory
    {
        private Factory()
        {
            
        }

        public readonly static Factory Instance = new Factory();

        public ICell CreateEmptyCell()
        {
            return Cell.Cell.CreateEmptyCell();
        }
    }
}
