using Mechanics.Cell;
using Mechanics.Field;

namespace Mechanics.FieldManager
{
    public interface IFieldManager
    {
        void Save(string fileName);
        IField SetCell(int x, int y, NumericValue value);
        IField CurrentField { get; }
    }
}