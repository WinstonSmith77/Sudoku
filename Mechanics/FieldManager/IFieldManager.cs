using Mechanics.Cell;
using Mechanics.Field;
using Mechanics.Geometry;

namespace Mechanics.FieldManager
{
    public interface IFieldManager
    {
        void Save(string fileName);
        IField SetCell(Point p, NumericValue value);
        IField CurrentField { get; }

        bool CanUndo();
        IField Undo();

        bool CanRedo();
        IField Redo();
    }
}