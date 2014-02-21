using Mechanics.Cell;
using Mechanics.Geometry;
using Mechanics.Grid;

namespace Mechanics.GridManager
{
    public interface IGridManager
    {
        void Save(string fileName);
        IGrid SetCell(Point p, NumericValue value);
        IGrid CurrentGrid { get; }

        bool CanUndo();
        IGrid Undo();

        bool CanRedo();
        IGrid Redo();
        bool CanReset();
    }
}