using Mechanics.Cell;

namespace Mechanics.Field
{
    public interface IField
    {
        ICell this[int x, int y] { get; }
    }
}