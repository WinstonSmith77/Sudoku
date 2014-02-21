using Mechanics.Cell;
using Mechanics.Grid;
using Mechanics.GridManager;

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

        public IGrid CreateEmptyGrid()
        {
            return Grid.Grid.CreateEmptyGrid();
        }

        public IGridManager CreateNewGridManager()
        {
            return GridManager.GridManager.CreateNewGridManager();
        }

        public IGridManager LoadGridManager(string fileName)
        {
            return GridManager.GridManager.Load(fileName);
        }
    }
}
