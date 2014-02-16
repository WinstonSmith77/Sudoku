using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mechanics.Cell;
using Mechanics.Field;
using Mechanics.FieldManager;

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

        public IField CreateEmptyField()
        {
            return Field.Field.CreateEmptyField();
        }

        public IFieldManager CreateEmptyFieldManager()
        {
            return FieldManager.FieldManager.CreateEmptyFieldManager();
        }

        public IFieldManager LoadFieldManager(string fileName)
        {
            return FieldManager.FieldManager.Load(fileName);
        }
    }
}
