using Mechanics.Cell;
using Mechanics.Field;

namespace Mechanics.FieldManager
{
    public interface IFieldManager
    {
        void Save(string fileName);
        IField SetCell(Point p, NumericValue value);
        IField CurrentField { get; }
    }
}